import * as React from "react";
import { Shimmer, ShimmerElementType as ElemType, ShimmerElementsGroup } from 'office-ui-fabric-react/lib/Shimmer';

export class DocumentCardShimmers extends React.Component<{}, {}> {
    
    public render() {

        const shimmerContent: JSX.Element = <div style={{ 
                                                    display: 'block',
                                                    borderWidth: 1,
                                                    borderStyle: 'solid',
                                                    borderColor: 'rgb(222, 222, 222)',
                                                    marginBottom: 15,
                                                    maxWidth: 318,
                                                    minWidth: 150                                              
                                                }}>
                                                <Shimmer 
                                                    customElementsGroup={
                                                        <ShimmerElementsGroup
                                                            shimmerElements={[
                                                                { type: ElemType.line, width: '100%', height: 196 },
                                                            ]}
                                                        />
                                                    } 
                                                    isDataLoaded={false}                  
                                                />                                                                                                   
                                                <div style={{
                                                    paddingTop: 8,
                                                    paddingRight: 16,
                                                    paddingBottom: 8,
                                                    paddingLeft: 16,
                                                }}>
                                                    <Shimmer 
                                                        customElementsGroup={
                                                            <div style={{ display: 'flex' }}>
                                                                <ShimmerElementsGroup
                                                                    flexWrap={true}
                                                                    width="100%"                                                                    
                                                                    shimmerElements={[
                                                                        { type: ElemType.line, width: '30%', height: 11 },
                                                                        { type: ElemType.gap, width: '70%', height: 11 },
                                                                        { type: ElemType.line, width: '100%', height: 13 },
                                                                        { type: ElemType.line, width: '60%', height: 13 },
                                                                        { type: ElemType.gap, width: '40%', height: 20 }
                                                                    ]}
                                                                />
                                                            </div>
                                                        } 
                                                        width="100%"
                                                        isDataLoaded={false}                                                                           
                                                    />                                                      
                                                </div>
                                                <div style={{
                                                    paddingTop: 8,
                                                    paddingRight: 16,
                                                    paddingBottom: 8,
                                                    paddingLeft: 16
                                                }}>
                                                <Shimmer 
                                                    customElementsGroup={
                                                        <div style={{ display: 'flex' }}>
                                                            <ShimmerElementsGroup
                                                                shimmerElements={[{ type: ElemType.circle, height: 32 }, { type: ElemType.gap, width: 10, height: 40 }]}
                                                            />
                                                            <ShimmerElementsGroup
                                                            flexWrap={true}
                                                            width="100%"
                                                            shimmerElements={[
                                                                { type: ElemType.line, width: '100%', height: 10 },
                                                                { type: ElemType.line, width: '75%', height: 10 },
                                                                { type: ElemType.gap, width: '25%', height: 20 }
                                                            ]}
                                                            />
                                                        </div>
                                                    } 
                                                    width="100%"
                                                    isDataLoaded={false}                                                                        
                                                />                                                
                                                </div>
                                                
                                            </div>;
        return shimmerContent;
    }
}