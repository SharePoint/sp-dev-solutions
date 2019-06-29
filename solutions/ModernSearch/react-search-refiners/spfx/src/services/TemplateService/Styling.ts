import {
    IDocumentCardStyleProps,
    IDocumentCardStyles,
    IDocumentCardPreviewStyleProps,
    IDocumentCardPreviewStyles,
    IDocumentCardTitleStyleProps,
    IDocumentCardImageStyleProps,
    IDocumentCardImageStyles,
    IDocumentCardActivityStyleProps,
    IDocumentCardActivityStyles,
  } from 'office-ui-fabric-react/lib/DocumentCard';
import { mergeStyleSets } from '@uifabric/merge-styles';
import * as documentCardGetStyles from 'office-ui-fabric-react/lib/components/DocumentCard/DocumentCard.styles';
import * as documentTitleGetStyles from 'office-ui-fabric-react/lib/components/DocumentCard/DocumentCardTitle.styles';
import * as documentCardPreviewGetStyles from 'office-ui-fabric-react/lib/components/DocumentCard/DocumentCardPreview.styles';
import * as documentCardImageGetStyles from 'office-ui-fabric-react/lib/components/DocumentCard/DocumentCardImage.styles';
import * as documentCardActivityGetStyles from 'office-ui-fabric-react/lib/components/DocumentCard/DocumentCardActivity.styles';
import * as personaCoinGetStyles from 'office-ui-fabric-react/lib/components/Persona/PersonaCoin/PersonaCoin.styles';
import * as personaImageGetStyles from 'office-ui-fabric-react/lib/components/Image/Image.styles';
import { IPersonaCoinStyleProps, IPersonaCoinStyles, PersonaSize } from 'office-ui-fabric-react/lib/PersonaCoin';
import { IImageStyleProps, IImageStyles, ImageFit } from 'office-ui-fabric-react/lib/components/Image';
import { classNamesFunction } from 'office-ui-fabric-react/lib/Utilities';
import { getTheme } from '@uifabric/styling';

export default class Styling {

    /***
     * Gets the default Office UI Fabric CSS class names used by React components to be able to use them in Handlebars templates
     * See https://github.com/OfficeDev/office-ui-fabric-react/wiki/Component-Styling
     */
    public static getOfficeUiTilesStyles(): any {

        // The DocumentCard component is an aggregation of multiple React components so we need to get styles for every individual components.
        // In the Handlebars template, you need then to follow the same HTML markup with associated classes to get the same experience as the complete React component.
        // Get the current loaded theme
        const theme = getTheme();

        // DocumentCard component styles
        const customDocumentCardStyles = {
            pnpDocumentCard: {
                marginBottom: 15
            }
        };

        const documentCardProps: IDocumentCardStyleProps = {
            actionable:true,
            compact: false,
            theme: theme
        };

        const documentCardStyles = mergeStyleSets(documentCardGetStyles.getStyles(documentCardProps), customDocumentCardStyles);
        const documentCardClassNames = classNamesFunction<IDocumentCardStyleProps, IDocumentCardStyles>()(documentCardStyles);

        // DocumentCardPreview component
        const customDocumentPreviewStyles = {
            pnpDocumentPreview: {
                selectors: {
                    ':hover': {
                      opacity: 0.3
                    }
                  }
            }
        };

        const documentCardPreviewProps: IDocumentCardPreviewStyleProps = {
            theme: theme,
            isFileList: false,
        };

        const documentCardPreviewStyles = mergeStyleSets(documentCardPreviewGetStyles.getStyles(documentCardPreviewProps), customDocumentPreviewStyles);
        const documentCardPreviewClassNames = classNamesFunction<IDocumentCardPreviewStyleProps, IDocumentCardPreviewStyles>()(documentCardPreviewStyles);

        // DocumentImage component
        const documentCardImageProps: IDocumentCardImageStyleProps = {
            theme: theme,
            imageFit: ImageFit.center
        };

        const documentCardImageStyles = documentCardImageGetStyles.getStyles(documentCardImageProps);
        const documentCardImageClassNames = classNamesFunction<IDocumentCardImageStyleProps, IDocumentCardImageStyles>()(documentCardImageStyles)

        // DocumentCardTitle component
        const documentCardTitleProps: IDocumentCardTitleStyleProps = {
            theme: theme
        };

        const documentCardTitleStyles = documentTitleGetStyles.getStyles(documentCardTitleProps);
        const documentCardTitleClassNames = classNamesFunction<IDocumentCardPreviewStyleProps, IDocumentCardPreviewStyles>()(documentCardTitleStyles)

        // DocumentActivity component
        const customActivityStyles = {
            pnpAdditionalCardDetails: {
                marginLeft: 56
            }
        };

        const documentCardActivityProps: IDocumentCardActivityStyleProps = {
            theme: theme,
            multiplePeople: false
        };

        const documentCardActivityStyles = mergeStyleSets(documentCardActivityGetStyles.getStyles(documentCardActivityProps),customActivityStyles)
        const documentCardActivityClassNames = classNamesFunction<IDocumentCardActivityStyleProps, IDocumentCardActivityStyles>()(documentCardActivityStyles);

        // PersonaCoin component
        const personaCoinProps: IPersonaCoinStyleProps = {
            size: PersonaSize.small,
            theme: theme,
            showUnknownPersonaCoin: true,
            coinSize: 32
        };

        const personaCoinStyles = personaCoinGetStyles.getStyles(personaCoinProps);
        const personaCoinClassNames = classNamesFunction<IPersonaCoinStyleProps, IPersonaCoinStyles>()(personaCoinStyles);
        
        const personaImageProps: IImageStyleProps  = {
            shouldFadeIn: true,
            isNotImageFit: true,
            height: 32,
            width: 32,            
            isLoaded: true,
            theme: theme,
            shouldStartVisible: true,
            isNone: true,
            maximizeFrame: true
        }

        const personaImageStyles = personaImageGetStyles.getStyles(personaImageProps); 
        const personaImageClassNames = classNamesFunction<IImageStyleProps, IImageStyles>()(personaImageStyles);

        const officeUiFabricStyles = {
            documentCard: documentCardClassNames,
            documentCardPreview: documentCardPreviewClassNames,
            documentCardTitle: documentCardTitleClassNames,
            documentCardImage: documentCardImageClassNames,
            documentCardActivity: documentCardActivityClassNames,
            personaCoin: personaCoinClassNames,
            personaImage: personaImageClassNames
        };    
        
        return officeUiFabricStyles;
    }
}