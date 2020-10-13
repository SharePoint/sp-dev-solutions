import * as React from 'react';
import { DefaultButton, CommandButton } from 'office-ui-fabric-react';
import { IWebPartContext } from '@microsoft/sp-webpart-base';
import { Logger, LogLevel } from "@pnp/logging";

import styles from './BoxButtonWebPart.module.scss';
import * as strings from 'boxButtonWebPartStrings';
import { IBoxButton } from '../BoxButtonWebPart';
import LinkPickerPanel from '../../../components/LinkPickerPanel/LinkPickerPanel';
import { LinkType } from '../../../components/LinkPickerPanel/ILinkPickerPanelProps';
import ElemUtil from "../../../utilities/element/elemUtil";
import { DisplayMode } from '@microsoft/sp-core-library';
import WebPartTitle from "../../../components/WebPartTitle/WebPartTitle";

const urlField = "URL";
const iconField = "Font Awesome Icon";
const isThemedField = "Has Blue Background";
const openNewTabField = "Open Link in New Tab";


export interface IBoxButtonProps {
  name: string;
  fontAwesomeIcon: string;
  url: string;
  isThemed: boolean;
  newTab: boolean;
  data: IBoxButton[];
  isEdit: boolean;
  title: string;
  usesListMode: boolean;
  advancedCamlQuery: string;
  advancedCamlData: string;
  links: any[];
  setTitle: (title: string) => void;
  setUrl: Function;
  editItem: Function;
  deleteItem: Function;
  rearrangeItems: Function;
  context: IWebPartContext;
  displayMode: DisplayMode;
}

export interface IBoxButtonState {
}

export default class BoxButton extends React.Component<IBoxButtonProps, IBoxButtonState> {
  private LOG_SOURCE = "BoxButton";
  private linkPickerPanel: LinkPickerPanel;

  private _dragElement: any;
  public get dragElement(): any {
    return this._dragElement;
  }
  public set dragElement(v: any) {
    this._dragElement = v;
  }

  private _mouseTarget: any;
  public get mouseTarget(): any {
    return this._mouseTarget;
  }
  public set mouseTarget(v: any) {
    this._mouseTarget = v;
  }

  private _eventDone: boolean;
  public get eventDone(): boolean {
    return this._eventDone;
  }
  public set eventDone(v: boolean) {
    this._eventDone = v;
  }

  public setTitle(event) {
    this.props.setTitle(event.target.value);
  }

  // ** Event handlers for link picker **

  // Open the link picker - called from onClick of Change (link) button
  public openLinkPicker = (event) => {
    if (this.linkPickerPanel) {
      this.linkPickerPanel.pickLink()
        .then(({ name, url }) => {
          this.props.setUrl(name, url);
        });
    }
  }

  // ** Event handlers for buttons **/
  // User clicks + button to add a link
  public addBox = (event) => {
    this.props.editItem(-1);
  }

  // User clicks edit button on a link
  public editBox = (event) => {
    try {
      event.stopPropagation();
      event.preventDefault();
      this.props.editItem(ElemUtil.closest(event.target, '[data-index]').getAttribute("data-index"));
    } catch (err) {
      Logger.write(`${err} - ${this.LOG_SOURCE} (editBox)`, LogLevel.Error);
    }
    return false;
  }

  // User clicks delete button on a link
  public deleteBox = (event) => {
    try {
      event.stopPropagation();
      event.preventDefault();
      if (confirm(strings.DeleteItemConfirmMessage))
        this.props.deleteItem(ElemUtil.closest(event.target, '[data-index]').getAttribute("data-index"));
    } catch (err) {
      Logger.write(`${err} - ${this.LOG_SOURCE} (deleteBox)`, LogLevel.Error);
    }
    return false;
  }

  public checkEventDone = (event) => {
    if (this.eventDone) {
      this.eventDone = false;
      return false;
    }
  }

  // Event handlers for drag and drop

  public mouseDragDown = (event) => {
    this.mouseTarget = event.target;
  }

  public startDrag = (event) => {
    try {
      event.stopPropagation();
      if (event.currentTarget.querySelector('#drag-handle').contains(this.mouseTarget)) {
        this.dragElement = event.currentTarget;
        event.dataTransfer.eventAllowed = "move";
        event.dataTransfer.setData('text/plan', 'drag-handle');
      }
      else {
        event.preventDefault();
      }
    } catch (err) {
      Logger.write(`${err} - ${this.LOG_SOURCE} (startDrag)`, LogLevel.Error);
    }
  }

  public isbefore(a, b) {
    if (a.parentNode == b.parentNode) {
      for (var cur = a; cur; cur = cur.previousSibling) {
        if (cur === b) {
          return true;
        }
      }
    }
    return false;
  }

  public endDrag = (event) => {
    try {
      const indexArr: number[] = [];
      const currentElements = ElemUtil.closest(event.currentTarget, '[data-reactroot]').querySelectorAll('[data-index]');
      currentElements.forEach((element) => { indexArr.push(parseInt(element.getAttribute('data-index'))); });
      this.props.rearrangeItems(indexArr);
    } catch (err) {
      Logger.write(`${err} - ${this.LOG_SOURCE} (endDrag)`, LogLevel.Error);
    }
  }

  public moveItem = (event) => {
    try {
      if (this.isbefore(this.dragElement, ElemUtil.closest(event.target, '[data-index]'))) {
        ElemUtil.closest(event.target, '[data-index]').parentNode.insertBefore(this.dragElement, ElemUtil.closest(event.target, '[data-index]'));
      }
      else {
        if (!this.dragElement.contains(ElemUtil.closest(event.target, '[data-index]')))
          ElemUtil.closest(event.target, '[data-index]').parentNode.insertBefore(this.dragElement, ElemUtil.closest(event.target, '[data-index]').nextSibling);
      }
    } catch (err) {
      Logger.write(`${err} - ${this.LOG_SOURCE} (moveItem)`, LogLevel.Error);
    }
  }

  // ** Render functions **

  public render(): React.ReactElement<IBoxButtonProps> {
    let body = (this.props.usesListMode) ? this.renderAdvancedWebPart() : this.renderBasicWebPart();
    // Insert retired web part message
    return (
      <>
        {(this.props.displayMode == DisplayMode.Edit) &&
          <div className={styles.editMode}>{strings.RetiredMessage}</div>
        }
        {body}
      </>
    );
  }

  // Render the "basic" web part with editable links
  public renderBasicWebPart(): JSX.Element {
    try {
      return (
        <div data-component="BoxButton-Basic">
          <WebPartTitle editMode={this.props.isEdit} title={this.props.title} updateTitle={this.props.setTitle} />
          {this.props.isEdit &&
            <CommandButton className={styles["new-item"]} iconProps={{ iconName: 'Add' }} onClick={this.addBox.bind(this)}>{strings.AddNewButtonText}</CommandButton>
          }
          {this.props.data.length > 0 && this.props.data.map((item) => {
            return this.renderBasicDefaultLayout(item);
          })
          }
          {this.props.data.length === 0 &&
            <div className={styles["box-link"]}>
              <div className={styles["empty-box"]}>
                <div role="button" onClick={this.openLinkPicker.bind(this)}>{strings.PlaceholderButtonText}</div>
              </div>
            </div>
          }
          {this.props.isEdit &&
            <LinkPickerPanel
              webPartContext={this.props.context}
              className={styles["link-picker"]}
              webAbsUrl={this.props.context.pageContext.web.absoluteUrl}
              linkType={LinkType.any}
              ref={(ref) => { this.linkPickerPanel = ref; }} />
          }
        </div>
      );
    } catch (err) {
      Logger.write(`${err} - ${this.LOG_SOURCE} (renderBasicWebPart)`, LogLevel.Error);
      return null;
    }
  }

  public renderBasicDefaultLayout(item: IBoxButton): JSX.Element {
    try {
      return (
        <div className={styles["box-link"]} role="link" id={"item-" + this.props.data.indexOf(item)} key={"item-" + this.props.data.indexOf(item)} draggable={this.props.isEdit} onDragStart={this.startDrag.bind(this)} onMouseDown={this.mouseDragDown.bind(this)} onDragEnter={this.moveItem.bind(this)} onDragEnd={this.endDrag.bind(this)} data-index={this.props.data.indexOf(item)}>
          {item.openNew &&
            <a href={item.url} target="blank" data-interception="off">
              <div className={styles["box-button"] + " " + (item.isBlue ? styles["themed"] : "") + " " + (this.props.isEdit ? styles["edit"] : "")}>
                <i className={item.icon ? "fa " + item.icon : ""}></i>
                {item.name}
              </div>
            </a>
          }
          {!item.openNew &&
            <a href={item.url}>
              <div className={styles["box-button"] + " " + (item.isBlue ? styles["themed"] : "") + " " + (this.props.isEdit ? styles["edit"] : "")}>
                <i className={item.icon ? "fa " + item.icon : ""}></i>
                {item.name}
              </div>
            </a>
          }
          {this.props.isEdit &&
            <div className={styles["edit-controls"]}>
              <DefaultButton iconProps={{ iconName: "Clear" }} onClick={this.deleteBox.bind(this)} />
              <DefaultButton iconProps={{ iconName: "Edit" }} onClick={this.editBox.bind(this)} />
              <i className="ms-Icon ms-Icon--Move" id="drag-handle" aria-hidden="true"></i>
            </div>
          }
        </div>);
    } catch (err) {
      Logger.write(`${err} - ${this.LOG_SOURCE} (renderBasicDefaultLayout)`, LogLevel.Error);
      return null;
    }
  }

  // Render the "advanced" web part, which is list-driven
  public renderAdvancedWebPart(): JSX.Element {
    try {
      return (
        <div data-component="BoxButton-Advanced">
          <WebPartTitle editMode={this.props.isEdit} title={this.props.title} updateTitle={this.props.setTitle} />
          {this.props.links.length > 0 && this.props.links.map((item) => {
            return this.renderAdvancedDefaultLayout(item);
          })}
        </div>
      );
    } catch (err) {
      Logger.write(`${err} - ${this.LOG_SOURCE} (renderAdvancedWebPart)`, LogLevel.Error);
      return null;
    }
  }

  public renderAdvancedDefaultLayout(item: any): JSX.Element {
    try {
      return (
        <div className={styles["box-link"]} role="link" key={"item-" + this.props.links.indexOf(item)}>
          {item[openNewTabField] &&
            <a href={item[urlField]} target="blank" data-interception="off">
              <div className={styles["box-button"] + " " + (item[isThemedField] ? styles["themed"] : "")}>
                <i className={item[iconField] ? "fa " + item[iconField] : ""}></i>
                {item[urlField + "_text"]}
              </div>
            </a>
          }
          {!item[openNewTabField] &&
            <a href={item[urlField]}>
              <div className={styles["box-button"] + " " + (item[isThemedField] ? styles["themed"] : "")}>
                <i className={item[iconField] ? "fa " + item[iconField] : ""}></i>
                {item[urlField + "_text"]}
              </div>
            </a>
          }
        </div>
      );
    } catch (err) {
      Logger.write(`${err} - ${this.LOG_SOURCE} (renderAdvancedDefaultLayout)`, LogLevel.Error);
      return null;
    }
  }
}
