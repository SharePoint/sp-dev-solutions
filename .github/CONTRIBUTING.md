# Contribution Guidance
If you'd like to contribute to this repository, please read the following guidelines. Contributors are more than welcome to share your learnings with others from centralized location.

## Code of Conduct
This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information, see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/)
or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

## Question or Problem?
Please do not open GitHub issues for general support questions as the GitHub list should be used for feature requests and bug reports. This way we can more easily track actual issues or bugs from the code and keep the general discussion separate from the actual code.  

If you have questions about how to use SharePoint Framework or any of the provided samples, please use the following locations.

* [SharePoint Developer Space](http://aka.ms/SPPnP-Community) at http://techcommunity.microsoft.com
* [SharePoint Stack Exchange](http://sharepoint.stackexchange.com/) with 'spfx' tag

## Typos, Issues, Bugs and contributions
Whenever you are submitting any changes to the SharePoint repositories, please follow these recommendations.

* Always fork repository to your own account and create a new branch with a unique name for applying modifications
* Do not combine multiple changes to one pull request, please submit for example any samples and documentation updates using separate PRs
* If you are submitting multiple sample solutions, please create specific PR for each of them
* If you are submitting typo or documentation fix, you can combine modifications to single PR where suitable

## Submitting changes as pull requests
Here's a high level process for submitting new samples or updates to existing ones.

1. Sign the Contributor License Agreement (see below)
2. Fork the main repository to your GitHub account
3. Create a new branch with a unique name based on the master branch, within your forked repo
4. Include your changes in the new branch
5. Commit your changes using descriptive commit message - These are used to track changes on the repositories for monthly communications, see [May 2017](https://dev.office.com/blogs/PnP-May-2017-Release) as an example
6. Create a pull request in your own fork and target 'master' branch
7. Fill out the provided PR template with the requested details

> note. Delete the feature specific branch only AFTER your pull request has been processed.

## Sample naming and structure guidelines
When you are submitting a new sample, it has to follow up below guidelines

- You will need to have a README file for your contribution, which is based on [provided template](../solutions/README-template.md) under the `solutions` folder. Please copy this template and update accordingly. README has to be named as README.md with capital letters.
    - You will need to have a picture of the web part in practice in the README file ("pics or it didn't happen"). Preview image must be located in /assets/ folder in the root your you solution.
- README template contains specific tracking image as a final entry in the page with img tag by default to https://telemetry.sharepointpnp.com/sp-dev-solutions/solutions/readme-template. This is transparent image, which is used to track popularity of individual samples in GitHub.
    - Updated the image src element according with repository name and folder information. If your sample is for example in samples folder and named as ChangeRequests, src element should be updated as https://telemetry.sharepointpnp.com/sp-dev-solutions/solutions/ChangeRequests
- If you find already similar kind of sample from the existing samples, we would appreciate you to rather extend existing one, than submitting a new similar sample
    - When you update existing samples, please update also README accordingly with information on provided changes and with your author details
- When you are submitting new sample solution, please name the sample solution folder accordingly
    - Name your folder based on the primary functionality of the component - for example, "ContactManagement"
    - Do not use words "sample", "solution", "extension", "webpart" or "wb" in the folder or sample name
- Do not use period/dot in the folder name of the provided sample

## Step-by-step on submitting a pull request to this repository
1. Navigate to https://github.com/[yourgitaccount]/sp-dev-solutions/pulls
2. Create a new Pull Request
3. Confirm the Source repo dropdown contains ```[yourgitaccount]/sp-dev-solution``` and the Source branch dropdown contains your newly created branch name
4. Confirm the Distination repo dropdown contains ```pnp/sp-dev-solution``` and the Destination branch dropdown contains ```master```
*![image](https://user-images.githubusercontent.com/38665906/125865172-f992585f-cdcc-412f-9601-0ffc0c6c19d1.png)
4. Fill out the provided PR template with the requested details

> note. Delete the feature specific branch only AFTER your pull request has been processed.

## Merging your existing github projects with this repository
If the sample you wish to contribute is stored in your own Github repository, you can use the following steps to merge it with the Psp-dev-solutions repository:

1. Fork the sp-dev-solutions repository from GitHub
2. Create a local git repository 
```
md sp-dev-solutions
cd sp-dev-solutions
git init
```
3. Pull your forked copy of sp-dev-solutions into your local repository
```
git remote add origin https://github.com/[yourgitaccount]/sp-dev-solutions.git
git pull origin master
```
4. Create a uniquely named branch within your local copy of sp-dev-solutions (confirm you are on the master branch)
```  
git branch [branchname]
git checkout [branchname]
```
5. Pull your other project from github into the samples folder of your local copy of sp-dev-solutions
```  
git subtree add --prefix=solutions/[projectname] https://github.com/[yourgitaccount]/projectname.git master
```
6. Push the changes up to your forked repository
```
git push origin [branchname]
```
7. Submit a new Pull Request (see instructions above)
        
## Signing the CLA
Before we can accept your pull requests you will be asked to sign electronically Contributor License Agreement (CLA), which is prerequisite for any contributions to PnP repository. This will be one time process, so for any future contributions you will not be asked to re-sign anything. After the CLA has been signed, our PnP core team members will have a look on your submission for final verification of the submission. Please do not delete your development branch until the submission has been closed.

You can find Microsoft CLA from the following address - https://cla.microsoft.com. 

Thank you for your contribution.  

> Sharing is caring. 
