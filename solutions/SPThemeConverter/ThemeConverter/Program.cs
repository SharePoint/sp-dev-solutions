using System;
using System.IO;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace ThemeConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            //ModernToClassic();  //TODO:  Add relevant file paths to this method and then uncomment.
            //ClassicToModern();  //TODO:  Add relevant file paths to this method and then uncomment.
            Console.WriteLine("Your conversion is complete!");
        }

        private static void ClassicToModern()
        {
            dynamic modern = new JObject();
            XmlDocument doc = new XmlDocument();
            doc.Load(@"[Add local path to SharePoint classic theme SPColor file]");
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("s", "http://schemas.microsoft.com/sharepoint/");
            XmlNode xmlNode = doc.DocumentElement.SelectSingleNode("//s:colorPalette/s:color[@name='AccentText']", nsmgr);
            modern.themePrimary = "#" + xmlNode.Attributes["value"].Value;
            xmlNode = doc.DocumentElement.SelectSingleNode("//s:colorPalette/s:color[@name='ButtonHoverBackground']", nsmgr);
            modern.themeLighterAlt = "#" + xmlNode.Attributes["value"].Value;
            xmlNode = doc.DocumentElement.SelectSingleNode("//s:colorPalette/s:color[@name='SelectionBackground']", nsmgr);
            modern.themeLighter = "#" + xmlNode.Attributes["value"].Value;
            xmlNode = doc.DocumentElement.SelectSingleNode("//s:colorPalette/s:color[@name='StrongLines']", nsmgr);
            modern.themeLight = "#" + xmlNode.Attributes["value"].Value;
            xmlNode = doc.DocumentElement.SelectSingleNode("//s:colorPalette/s:color[@name='SuiteBarHoverBackground']", nsmgr);
            modern.themeTertiary = "#" + xmlNode.Attributes["value"].Value;
            xmlNode = doc.DocumentElement.SelectSingleNode("//s:colorPalette/s:color[@name='AccentLines']", nsmgr);
            modern.themeSecondary = "#" + xmlNode.Attributes["value"].Value;
            xmlNode = doc.DocumentElement.SelectSingleNode("//s:colorPalette/s:color[@name='EmphasisHoverBackground']", nsmgr);
            modern.themeDarkAlt = "#" + xmlNode.Attributes["value"].Value;
            xmlNode = doc.DocumentElement.SelectSingleNode("//s:colorPalette/s:color[@name='HyperlinkActive']", nsmgr);
            modern.themeDark = "#" + xmlNode.Attributes["value"].Value;
            xmlNode = doc.DocumentElement.SelectSingleNode("//s:colorPalette/s:color[@name='NavigationPressed']", nsmgr);
            modern.themeDarker = "#" + xmlNode.Attributes["value"].Value;
            xmlNode = doc.DocumentElement.SelectSingleNode("//s:colorPalette/s:color[@name='ButtonBackground']", nsmgr);
            modern.neutralLighterAlt = "#" + xmlNode.Attributes["value"].Value;
            xmlNode = doc.DocumentElement.SelectSingleNode("//s:colorPalette/s:color[@name='NavigationSelectedBackground']", nsmgr);
            modern.neutralLighter = "#" + xmlNode.Attributes["value"].Value;
            xmlNode = doc.DocumentElement.SelectSingleNode("//s:colorPalette/s:color[@name='DisabledLines']", nsmgr);
            modern.neutralLight = "#" + xmlNode.Attributes["value"].Value;
            xmlNode = doc.DocumentElement.SelectSingleNode("//s:colorPalette/s:color[@name='HeaderFlyoutBorder']", nsmgr);
            modern.neutralQuaternaryAlt = "#" + xmlNode.Attributes["value"].Value;
            modern.neutralQuaternary = "#" + xmlNode.Attributes["value"].Value; //No Designer setting available for neutralQuaternary.
            xmlNode = doc.DocumentElement.SelectSingleNode("//s:colorPalette/s:color[@name='SubtleLines']", nsmgr);
            modern.neutralTertiaryAlt = "#" + xmlNode.Attributes["value"].Value;
            xmlNode = doc.DocumentElement.SelectSingleNode("//s:colorPalette/s:color[@name='DisabledText']", nsmgr);
            modern.neutralTertiaryAlt = "#" + xmlNode.Attributes["value"].Value;
            xmlNode = doc.DocumentElement.SelectSingleNode("//s:colorPalette/s:color[@name='Lines']", nsmgr);
            modern.neutralSecondary = "#" + xmlNode.Attributes["value"].Value;
            xmlNode = doc.DocumentElement.SelectSingleNode("//s:colorPalette/s:color[@name='SubtleBodyText']", nsmgr);
            modern.neutralPrimaryAlt = "#" + xmlNode.Attributes["value"].Value;
            xmlNode = doc.DocumentElement.SelectSingleNode("//s:colorPalette/s:color[@name='BodyText']", nsmgr);
            modern.neutralPrimary = "#" + xmlNode.Attributes["value"].Value;
            xmlNode = doc.DocumentElement.SelectSingleNode("//s:colorPalette/s:color[@name='Navigation']", nsmgr);
            modern.neutralDark = "#" + xmlNode.Attributes["value"].Value;
            xmlNode = doc.DocumentElement.SelectSingleNode("//s:colorPalette/s:color[@name='SiteTitle']", nsmgr);
            modern.black = "#" + xmlNode.Attributes["value"].Value;
            xmlNode = doc.DocumentElement.SelectSingleNode("//s:colorPalette/s:color[@name='PageBackground']", nsmgr);
            modern.white = "#" + xmlNode.Attributes["value"].Value;
            File.WriteAllText(@"[Add local path to SharePoint modern theme JSON file output]", modern.ToString());
        }

        private static void ModernToClassic()
        {
            JObject modern = JObject.Parse(File.ReadAllText(@"[Add local path to SharePoint modern theme JSON file]"));

            using (var s = new StreamWriter(@"[Add local path to SharePoint classic theme SPColor file output]"))
            {
                s.WriteLine("<?xml version=\"1.0\" encoding=\"utf - 8\"?>");
                s.WriteLine("<s:colorPalette isInverted=\"true\" previewSlot1=\"BackgroundOverlay\" previewSlot2=\"BodyText\" " +
                    "previewSlot3=\"AccentText\" xmlns:s=\"http://schemas.microsoft.com/sharepoint/\">");
                s.WriteLine("<s:color name=\"BodyText\" value=\"" + modern["neutralPrimary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"SubtleBodyText\" value=\"" + modern["neutralPrimaryAlt"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"StrongBodyText\" value=\"" + modern["black"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"DisabledText\" value=\"" + modern["neutralTertiary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"SiteTitle\" value=\"" + modern["black"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"WebPartHeading\" value=\"" + modern["neutralPrimary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"ErrorText\" value=\"ff5f5f\" />"); //Change this to #a4262c, if inverted (dark background).
                s.WriteLine("<s:color name=\"AccentText\" value=\"" + modern["themePrimary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"SearchURL\" value=\"" + modern["neutralPrimary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"Hyperlink\" value=\"" + modern["themePrimary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"Hyperlinkfollowed\" value=\"" + modern["themePrimary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"HyperlinkActive\" value=\"" + modern["themeDark"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"CommandLinks\" value=\"" + modern["neutralDark"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"CommandLinksSecondary\" value=\"" + modern["black"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"CommandLinksHover\" value=\"" + modern["themePrimary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"CommandLinksPressed\" value=\"" + modern["themeDarker"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"CommandLinksDisabled\" value=\"" + modern["neutralTertiary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"BackgroundOverlay\" value=\"" + modern["white"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"DisabledBackground\" value=\"" + modern["neutralLighterAlt"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"PageBackground\" value=\"" + modern["white"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"HeaderBackground\" value=\"" + modern["white"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"FooterBackground\" value=\"" + modern["white"].ToString().Replace("#", "") + "\" />");
                //List/Library Items
                s.WriteLine("<s:color name=\"SelectionBackground\" value=\"" + modern["themeLighter"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"HoverBackground\" value=\"" + modern["themeLighter"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"RowAccent\" value=\"" + modern["themePrimary"].ToString().Replace("#", "") + "\" />");

                s.WriteLine("<s:color name=\"StrongLines\" value=\"" + modern["themeLight"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"Lines\" value=\"" + modern["neutralSecondary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"SubtleLines\" value=\"" + modern["neutralTertiaryAlt"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"DisabledLines\" value=\"" + modern["neutralLight"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"AccentLines\" value=\"" + modern["themeSecondary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"DialogBorder\" value=\"" + modern["neutralLight"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"Navigation\" value=\"" + modern["neutralDark"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"NavigationAccent\" value=\"" + modern["themePrimary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"NavigationHover\" value=\"" + modern["themePrimary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"NavigationPressed\" value=\"" + modern["themeDarker"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"NavigationHoverBackground\" value=\"" + modern["themeLighter"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"NavigationSelectedBackground\" value=\"" + modern["neutralLighter"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"EmphasisText\" value=\"" + modern["white"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"EmphasisBackground\" value=\"" + modern["themePrimary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"EmphasisHoverBackground\" value=\"" + modern["themeDarkAlt"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"EmphasisBorder\" value=\"" + modern["themeDarkAlt"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"EmphasisHoverBorder\" value=\"" + modern["themeDarker"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"SubtleEmphasisText\" value=\"" + modern["neutralDark"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"SubtleEmphasisCommandLinks\" value=\"" + modern["black"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"SubtleEmphasisBackground\" value=\"" + modern["neutralLighter"].ToString().Replace("#", "") + "\" />");
                //Top Bar
                s.WriteLine("<s:color name=\"TopBarText\" value=\"" + modern["neutralDark"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"TopBarBackground\" value=\"" + modern["neutralLighter"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"TopBarHoverText\" value=\"" + modern["black"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"TopBarPressedText\" value=\"" + modern["black"].ToString().Replace("#", "") + "\" />");

                s.WriteLine("<s:color name=\"HeaderText\" value=\"" + modern["neutralPrimary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"HeaderSubtleText\" value=\"" + modern["neutralPrimaryAlt"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"HeaderDisableText\" value=\"" + modern["neutralTertiary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"HeaderNavigationText\" value=\"" + modern["neutralDark"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"HeaderNavigationHoverText\" value=\"" + modern["themePrimary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"HeaderNavigationPressedText\" value=\"" + modern["themeDarker"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"HeaderNavigationSelectedText\" value=\"" + modern["themePrimary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"HeaderLines\" value=\"" + modern["neutralSecondary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"HeaderStrongLines\" value=\"" + modern["themeLight"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"HeaderAccentLines\" value=\"" + modern["themeSecondary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"HeaderSubtleLines\" value=\"" + modern["neutralTertiaryAlt"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"HeaderDisabledLines\" value=\"" + modern["neutralLight"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"HeaderDisabledBackground\" value=\"" + modern["neutralLighterAlt"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"HeaderFlyoutBorder\"  value=\"" + modern["neutralQuaternaryAlt"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"HeaderSiteTitle\" value=\"" + modern["black"].ToString().Replace("#", "") + "\" />");
                //Suite Bar
                s.WriteLine("<s:color name=\"SuiteBarBackground\" value=\"" + modern["themePrimary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"SuiteBarHoverBackground\" value=\"" + modern["themeTertiary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"SuiteBarText\" value=\"" + modern["white"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"SuiteBarDisabledText\" value=\"" + modern["neutralTertiaryAlt"].ToString().Replace("#", "") + "\" />");

                s.WriteLine("<s:color name=\"ButtonText\" value=\"" + modern["neutralPrimary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"ButtonDisabledText\" value=\"" + modern["neutralTertiary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"ButtonBackground\" value=\"" + modern["neutralLighterAlt"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"ButtonHoverBackground\" value=\"" + modern["themeLighterAlt"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"ButtonPressedBackground\" value=\"" + modern["themeLight"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"ButtonDisabledBackground\" value=\"" + modern["neutralLighterAlt"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"ButtonBorder\" value=\"" + modern["neutralSecondary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"ButtonHoverBorder\" value=\"" + modern["themeLight"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"ButtonPressedBorder\" value=\"" + modern["themeSecondary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"ButtonDisabledBorder\" value=\"" + modern["neutralLight"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"ButtonGlyph\" value=\"" + modern["neutralDark"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"ButtonGlyphActive\" value=\"" + modern["neutralPrimary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"ButtonGlyphDisabled\" value=\"" + modern["neutralTertiaryAlt"].ToString().Replace("#", "") + "\" />");
                //Tiles
                s.WriteLine("<s:color name=\"TileText\" value=\"" + modern["white"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"TileBackgroundOverlay\" value=\"" + modern["themeDarker"].ToString().Replace("#", "") + "\" />");
                //Accent Content
                s.WriteLine("<s:color name=\"ContentAccent1\" value=\"" + modern["themePrimary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"ContentAccent2\" value=\"" + modern["themePrimary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"ContentAccent3\" value=\"" + modern["themePrimary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"ContentAccent4\" value=\"" + modern["themePrimary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"ContentAccent5\" value=\"" + modern["themePrimary"].ToString().Replace("#", "") + "\" />");
                s.WriteLine("<s:color name=\"ContentAccent6\" value=\"" + modern["themePrimary"].ToString().Replace("#", "") + "\" />");

                s.WriteLine("</s:colorPalette>");
                s.Flush();
                s.Close();
            }
        }
    }
}
