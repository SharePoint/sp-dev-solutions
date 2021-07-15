# SharePointWorkflowConverter
A proof-of-concept C# console application for retrieving legacy 2010 & 2013 custom SharePoint workflow definition files, and creating new Power Automate flows programmatically.

### General Notes:
1. This project is a proof-of-concept to demonstrate that it's *possible* to programmatically retrieve/parse SharePoint legacy workflow definition files and create new Power Automate flows.  As for specific use cases and implementation, this is a launch pad for you to build those out.   
1. Only compatible with SharePoint Online.
2. Tested with a tenant [global admin](https://docs.microsoft.com/en-us/microsoft-365/admin/add-users/about-admin-roles?view=o365-worldwide#commonly-used-microsoft-365-admin-center-roles) account.
3. Uses the 4.7.2 [.NET Framework](https://docs.microsoft.com/en-us/dotnet/standard/choosing-core-framework-server).  
4. The code for retrieving legacy workflows and the code for creating a new Power Automate flow are contained in 2 separate respective functions: `GetLegacyWorkflow()` and `CreateNewWorkflow()`.  If you don't need one or the other for your purposes, simply comment them out.
### Notes about retrieving legacy 2010/2013 workflows:
1. [Legacy authentication](https://techcommunity.microsoft.com/t5/microsoft-sharepoint-blog/sharepoint-online-authentication-in-powershell-for-csom-when/ba-p/510114) must be enabled in the tenant for this code to work.   
2. All custom list/library workflows created within SharePoint Designer will have `*.xoml` and `*.xoml.wfconfig.xml` files.  Each file contains pertinent information describing the workflow, such as list/library associations, actions, etc.  You can manually view these files in [SharePoint Designer 2013](https://www.microsoft.com/en-us/download/details.aspx?id=35491) by clicking *All Files* from the left-hand navigation and selecting > *Workflows*.  The files can be programmatically retrieved using this project and parsed to map to equivalent Power Automate connections, actions, etc.
![SharePoint Designer Screenshot](/SS.png?raw=true)
3. Declarative `*.xoml` and `*.xoml.wfconfig.xml` files do not appear to exist for workflows created from [OOB SharePoint 2010 or 2013 workflow templates](https://support.microsoft.com/en-us/office/overview-of-workflows-included-with-sharepoint-d74fcceb-3a64-40fb-9904-cc33ca49da56).  *(If you can find them - I'll buy you a beverage of your choice!)*
### Notes about creating Power Automate flows:
1. This project leverages the [Power Automate Management Web API](https://docs.microsoft.com/en-us/power-automate/web-api) to create a new Power Automate flow.  
2. Per Microsoft's [documentation](https://docs.microsoft.com/en-us/power-automate/web-api), flows shown in the *My Flows* tab are not supported by these APIs.  Instead you will find flows created via the API in your *[Solutions](https://flow.microsoft.com/en-us/blog/solutions-in-microsoft-flow/) > Default Solution* tab.  
*(I have opened a [GitHub issue](https://github.com/MicrosoftDocs/power-automate-docs/issues/323) inquiring about how to programmatically create Power Automate flows within solutions other than Default.)* 
3. The meat and potatoes of the flow payload is in the `clientdata` property.  To help determine appropriate content for this section, I recommend manually creating the desired Power Automate flow using the UI.  Then, you can [leverage the API](https://docs.microsoft.com/en-us/power-automate/web-api#list-flows) to get the `clientdata` content for your manual flow and copy/paste it into the payload for your new flow.  
*(Also a great strategy for programmatically bulk-creating Power Automate flows in an environment!)*
4.  Once you successfully create a new Power Automate flow via the API, you will need to turn it on.  Then, do a Ctrl+F5 refresh to confirm your connections appear as expected.   
![Power Automate Screenshot](/SS1.png?raw=true)

### Happy Automating! :-)
