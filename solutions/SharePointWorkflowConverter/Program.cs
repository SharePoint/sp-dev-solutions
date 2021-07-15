using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ConvertWorkflowFW
{
    class Program
    {
        /*****************
         * TODO:  Replace user, password, and SharePoint Online URL.
         */
        private static readonly HttpClient client = new HttpClient();
        private static readonly string user = "username@domain.onmicrosoft.com";
        private static readonly string password = "password";
        private static readonly string url = "https://domain.sharepoint.com";
        static async Task Main(string[] args)
        {
            await GetLegacyWorkflow();  //Legacy Authentication must be enabled within tenant(https://techcommunity.microsoft.com/t5/microsoft-sharepoint-blog/sharepoint-online-authentication-in-powershell-for-csom-when/ba-p/510114)
            await CreateNewWorkflow();
        }

        /*****************
         * TODO:  Enable Legacy Authentication
         *        Replace workflow site collection URL, workflow name
         */
        private static async Task GetLegacyWorkflow()
        {
            string workflowSiteCollectionURL = "/sites/SiteCollectionName";
            string workflowName = "TestWorkflow";
            /*****************
             * Get Security Token for required FedAuth and rtFa cookies.
             */
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<s:Envelope xmlns:s='http://www.w3.org/2003/05/soap-envelope'" +
                        " xmlns:a='http://www.w3.org/2005/08/addressing'" +
                        " xmlns:u='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd'>" +
                        "<s:Header>" +
                        "<a:Action s:mustUnderstand='1'>http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Issue</a:Action>" +
                        "<a:ReplyTo>" +
                        "<a:Address>http://www.w3.org/2005/08/addressing/anonymous</a:Address>" +
                        "</a:ReplyTo>" +
                        "<a:To s:mustUnderstand='1'>https://login.microsoftonline.com/extSTS.srf</a:To>" +
                        "<o:Security s:mustUnderstand='1'" +
                        " xmlns:o='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd'>" +
                        "<o:UsernameToken>" +
                        "<o:Username>" + user + "</o:Username>" +
                        "<o:Password>" + password + "</o:Password>" +
                        "</o:UsernameToken>" +
                        "</o:Security>" +
                        "</s:Header>" +
                        "<s:Body>" +
                        "<t:RequestSecurityToken xmlns:t='http://schemas.xmlsoap.org/ws/2005/02/trust'>" +
                        "<wsp:AppliesTo xmlns:wsp='http://schemas.xmlsoap.org/ws/2004/09/policy'>" +
                        "<a:EndpointReference>" +
                        "<a:Address>" + url + workflowSiteCollectionURL + "</a:Address>" +
                        "</a:EndpointReference>" +
                        "</wsp:AppliesTo>" +
                        "<t:KeyType>http://schemas.xmlsoap.org/ws/2005/05/identity/NoProofKey</t:KeyType>" +
                        "<t:RequestType>http://schemas.xmlsoap.org/ws/2005/02/trust/Issue</t:RequestType>" +
                        "<t:TokenType>urn:oasis:names:tc:SAML:1.0:assertion</t:TokenType>" +
                        "</t:RequestSecurityToken>" +
                        "</s:Body>" +
                        "</s:Envelope>");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://login.microsoftonline.com/extSTS.srf");
            request.Content = new StringContent(doc.InnerXml, Encoding.UTF8, "application/xml");
            HttpResponseMessage response;
            response = await client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            doc = new XmlDocument();
            doc.LoadXml(responseString);
            XmlNodeList nodeList = doc.GetElementsByTagName("wsse:BinarySecurityToken");
            string token = string.Empty;
            foreach (XmlNode node in nodeList)
            {
                token = node.InnerText;
            }
            /*****************
             * Send Security Token to get FedAuth and rtFa cookies.
             */
            request = new HttpRequestMessage(HttpMethod.Post, url + "/_forms/default.aspx?wa=wsignin1.0");
            request.Headers.Add("Accept", "application/json; odata=verbose");
            request.Content = new StringContent(token);
            response = await client.SendAsync(request);

            /*****************
             * Since we are using the same HttpClient object, the FedAuth and rtFa cookies are now contained in it.
             * Get the .xoml file for the relevant workflow.  
             */
            request = new HttpRequestMessage(HttpMethod.Post, url + workflowSiteCollectionURL + "/_vti_bin/_vti_aut/author.dll");
            request.Headers.Add("X-Vermeer-Content-Type", "application/x-www-form-urlencoded");
            request.Content = new StringContent("method=get+document:15.0.0.4455&service_name=" +
                                                    workflowSiteCollectionURL +
                                                    "&document_name=Workflows/" + workflowName + "/" +
                                                    workflowName +
                                                    ".xoml&old_theme_html=false&force=true&get_option=none&doc_version=&timeout=0&expandWebPartPages=true",
                                                Encoding.UTF8, "application/x-www-form-urlencoded");

            response = await client.SendAsync(request);
            responseString = await response.Content.ReadAsStringAsync();

            /*****************
             * Since we are using the same HttpClient object, the FedAuth and rtFa cookies are now contained in it.
             * Get the .xoml.wfconfig.xml file for the relevant workflow.  
             */
            request = new HttpRequestMessage(HttpMethod.Post, url + workflowSiteCollectionURL + "/_vti_bin/_vti_aut/author.dll");
            request.Headers.Add("X-Vermeer-Content-Type", "application/x-www-form-urlencoded");
            request.Content = new StringContent("method=get+document:15.0.0.4455&service_name=" +
                                                    workflowSiteCollectionURL +
                                                    "&document_name=Workflows/" + workflowName + "/" +
                                                    workflowName +
                                                    ".xoml.wfconfig.xml&old_theme_html=false&force=true&get_option=none&doc_version=&timeout=0&expandWebPartPages=true",
                                                Encoding.UTF8, "application/x-www-form-urlencoded");

            response = await client.SendAsync(request);
            responseString = await response.Content.ReadAsStringAsync();
        }

        /*****************
         * TODO:  Create app registration for CDS authentication(https://docs.microsoft.com/en-us/powerapps/developer/common-data-service/walkthrough-register-app-azure-active-directory#create-an-application-registration),
         *        Replace clientID with app registration Client Id,
         *        Replace Power Automate Web API URL(https://docs.microsoft.com/en-us/power-automate/web-api#compose-http-requests),
         *        Replace clientData with content from relevant Power Automate flow(https://docs.microsoft.com/en-us/power-automate/web-api#list-flows)
         */
        private static async Task CreateNewWorkflow()
        {
            string resource = "https://org00000000.crm.dynamics.com";
            string authorityURI = "https://login.microsoftonline.com/common";
            string clientID = "000000000-0000-0000-0000-000000000";
            Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext authContext = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(authorityURI, false);
            UserPasswordCredential credentials = new UserPasswordCredential(user, password);
            AuthenticationResult authResult = await authContext.AcquireTokenAsync(resource, clientID, credentials);
            string accessToken = authResult.AccessToken;
            //Replace with clientData from preferred Power Automate flow.
            string clientData = "{\"properties\":{\"connectionReferences\":{\"shared_commondataservice\":{\"connectionName\":\"shared-commondataser-00000000-0000-0000-0000-000000000004\",\"source\":\"Invoker\",\"id\":\"/providers/Microsoft.Power Apps/apis/shared_commondataservice\"}},\"definition\":{\"$schema\": \"https://schema.management.azure.com/providers/Microsoft.Logic/schemas/2016-06-01/workflowdefinition.json#\",\"contentVersion\": \"1.0.0.0\",\"parameters\": {\"$connections\": {\"defaultValue\": {},\"type\": \"Object\"},\"$authentication\": {\"defaultValue\": {},\"type\": \"SecureObject\"}},\"triggers\": {\"Recurrence\": {\"recurrence\": {\"frequency\": \"Minute\",\"interval\": 1},\"type\": \"Recurrence\"}},\"actions\": {\"List_records\": {\"runAfter\": {},\"metadata\": {\"flowSystemMetadata\": {\"swaggerOperationId\": \"GetItems_V2\"}},\"type\": \"ApiConnection\",\"inputs\": {\"host\": {\"api\": {\"runtimeUrl\": \"https://firstrelease-001.azure-apim.net/apim/commondataservice\"},\"connection\": {\"name\": \"@parameters('$connections')['shared_commondataservice']['connectionId']\"}},\"method\": \"get\",\"path\": \"/v2/datasets/@{encodeURIComponent(encodeURIComponent('default.cds'))}/tables/@{encodeURIComponent(encodeURIComponent('accounts'))}/items\",\"queries\": {\"$top\": 1},\"authentication\": \"@parameters('$authentication')\"}}},\"outputs\": {}}},\"schemaVersion\":\"1.0.0.0\"}";
            WorkFlow workflow = new WorkFlow
            {
                category = 5,
                statecode = 0,
                name = "Sample Flow",
                type = 1,
                description = "Sample flow to test programmatic creation.",
                primaryentity = "none",
                clientdata = clientData
            };
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, resource + "/api/data/v9.1/workflows");
            request.Content = new StringContent(JsonSerializer.Serialize(workflow), Encoding.UTF8, "application/json");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = client.SendAsync(request).Result;
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Error creating workflow:  " + response.StatusCode + " - " + response.ReasonPhrase);
            }
        }
    }
    public class WorkFlow
    {
        public int category { get; set; }
        public int statecode { get; set; }
        public string name { get; set; }
        public int type { get; set; }
        public string description { get; set; }
        public string primaryentity { get; set; }
        public string clientdata { get; set; }
    }
}
