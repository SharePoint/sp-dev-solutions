# SPTheme-Converter
Convert SharePoint modern themes to classic and classic themes to modern.

1. Open the Program.cs file and add input and output local path values for the relevant conversion method(s) you need:  ```ClassicToModern()``` or ```ModernToClassic()```.
2. Uncomment the relevant conversion method(s) you need.
3. Run the program.

For reference, here are the mapped values between SharePoint [Modern](https://github.com/OfficeDev/office-ui-fabric-react/wiki/Theming) and [Classic](https://docs.microsoft.com/en-us/sharepoint/dev/solution-guidance/use-composed-looks-to-brand-sharepoint-sites) theme palettes: 

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
| neutralQuaternary | No [Designer](https://fabricweb.z5.web.core.windows.net/pr-deploy-site/refs/heads/master/theming-designer/index.html) setting available |
| neutralTertiaryAlt | 
