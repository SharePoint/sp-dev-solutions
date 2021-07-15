# SPTheme-Converter
A C# console application to convert a classic SharePoint theme [SP color file]( https://docs.microsoft.com/en-us/sharepoint/dev/solution-guidance/use-composed-looks-to-brand-sharepoint-sites) into a modern UI Fabric Theme Designer [JSON file](https://fabricweb.z5.web.core.windows.net/pr-deploy-site/refs/heads/master/theming-designer/index.html), and vice versa. 

### NOTE:  This application only works with valid [SPColor file](https://docs.microsoft.com/en-us/sharepoint/dev/solution-guidance/use-composed-looks-to-brand-sharepoint-sites) and UI Fabric Theme Designer [JSON file](https://fabricweb.z5.web.core.windows.net/pr-deploy-site/refs/heads/master/theming-designer/index.html) inputs.
1. Open the Program.cs file and add input and output local path values for the relevant conversion method(s) you need:  ```ClassicToModern()``` or ```ModernToClassic()```.
2. Uncomment the relevant conversion method(s) you need.
3. Run the program.

The [SharePoint (classic) Color Palette Tool](https://www.microsoft.com/en-us/download/details.aspx?id=38182) exposes ~90 color slot settings.  The [UI Fabric Theme Designer](https://fabricweb.z5.web.core.windows.net/pr-deploy-site/refs/heads/master/theming-designer/index.html) has trimmed that number down to 21 color slot settings.  (There are actually 22 slots that appear in an exported theme files, but only 21 of them are available in the UI application.)

For reference, here are the mapped values between SharePoint [Modern](https://github.com/OfficeDev/office-ui-fabric-react/wiki/Theming) and [Classic](https://docs.microsoft.com/en-us/sharepoint/dev/solution-guidance/use-composed-looks-to-brand-sharepoint-sites) theme palette slots: 

| Modern Palette Slots  | Classic Palette Slots |
| ------------- | ------------- |
| themePrimary  | AccentText<br/>Hyperlink<br/>Hyperlinkfollowed<br/>CommandLinksHoverRowAccent<br/>NavigationAccent<br/>NavigationHover<br/>EmphasisBackground<br/>HeaderNavigationHoverText<br/>HeaderNavigationSelectedText<br/>SuiteBarBackground<br/>ContentAccent1<br/>ContentAccent2<br/>ContentAccent3<br/>ContentAccent4<br/>ContentAccent5<br/>ContentAccent6 |
| neutralPrimary  | BodyText<br/>WebPartHeading<br/>SearchURL<br/>HeaderText<br/>ButtonText<br/>ButtonGlyphActive  |
| white | BackgroundOverlay<br/>PageBackground<br/>HeaderBackground<br/>FooterBackground<br/>EmphasisText<br/>SuiteBarText<br/>TileText |
| themeLighterAlt | ButtonHoverBackground |
| themeLighter | SelectionBackground<br/>HoverBackground<br/>NavigationHoverBackground |
| themeLight | StrongLines<br/>HeaderStrongLines<br/>ButtonPressedBackground<br/>ButtonHoverBorder |
| themeTertiary | SuiteBarHoverBackground |
| themeSecondary | AccentLines<br/>HeaderAccentLines<br/>ButtonPressedBorder |
| themeDarkAlt | EmphasisHoverBackground<br/>EmphasisBorder |
| themeDark | HyperlinkActive |
| themeDarker | CommandLinksPressed<br/>NavigationPressed<br/>EmphasisHoverBorder<br/>HeaderNavigationPressedText<br/>TileBackgroundOverlay |
| neutralLighterAlt | DisabledBackground<br/>HeaderDisabledBackground<br/>ButtonBackground<br/>ButtonDisabledBackground |
| neutralLighter | NavigationSelectedBackground<br/>SubtleEmphasisBackground<br/>TopBarBackground |
| neutralLight | DisabledLines<br/>DialogBorder<br/>HeaderDisabledLines<br/>ButtonDisabledBorder | 
| neutralQuaternaryAlt | HeaderFlyoutBorder |
| neutralQuaternary | *No [Modern Designer](https://fabricweb.z5.web.core.windows.net/pr-deploy-site/refs/heads/master/theming-designer/index.html) setting available* |
| neutralTertiaryAlt | SubtleLines<br/>HeaderSubtleLines<br/>SuiteBarDisabledText<br/>ButtonGlyphDisabled |
| neutralTertiary | DisabledText<br/>CommandLinksDisabled<br/>HeaderDisableText<br/>ButtonDisabledText |
| neutralSecondary | Lines<br/>HeaderLines<br/>ButtonBorder |
| neutralPrimaryAlt | SubtleBodyText<br/>HeaderSubtleText |
| neutralDark | CommandLinks<br/>Navigation<br/>SubtleEmphasisText<br/>TopBarText<br/>HeaderNavigationText<br/>ButtonGlyph |
| black | StrongBodyText<br/>SiteTitle<br/>CommandLinksSecondary<br/>SubtleEmphasisCommandLinks<br/>TopBarHoverText<br/>TopBarPressedText<br/>HeaderSiteTitle |
## Converted Classic Theme (Preview) 
![Preview of Classic Theme](https://github.com/TracyGH/SPTheme-Converter/blob/master/classicpreview.png)

## Converted Modern Theme (Team Site Home Page)
![Modern Team Home Page](https://github.com/TracyGH/SPTheme-Converter/blob/master/modernteam.png)
