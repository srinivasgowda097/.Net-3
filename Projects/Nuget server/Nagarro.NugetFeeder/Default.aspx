<%@ Page Language="C#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Nagarro Nuget Repository</title>
    <style>
        body { font-family: Calibri; }
    </style>
</head>
<body>
    <div>
        <h1>
            <img src="Content/Images/Nagarro.png" />
            <img src="Content/Images/nugetlogo.png" /><img src="Content/Images/beta.png" /></h1>
        <h4>You are running NuGet.Server v<%= typeof(NuGet.Server.DataServices.Package).Assembly.GetName().Version %></h4>
        <p>
            Click <a href="<%= VirtualPathUtility.ToAbsolute("~/nuget/Packages") %>">here</a> to view your packages.
        </p>
        <fieldset style="width:800px">
            <legend><strong>Repository URLs</strong></legend>
            In the package manager settings, add the following URL to the list of 
            Package Sources:
            <blockquote>
                <strong><%= Helpers.GetRepositoryUrl(Request.Url, Request.ApplicationPath) %></strong>
            </blockquote>
            <% if (String.IsNullOrEmpty(ConfigurationManager.AppSettings["apiKey"])) { %>
            To enable pushing packages to this feed using the nuget command line tool (nuget.exe). Set the api key appSetting in web.config.
            <% } %> 
            <% else { %>
            Use the command below to push packages to this feed using the nuget command line tool (nuget.exe).
            <% } %>
            <blockquote>
                <strong>nuget push {package file} -s <%= Helpers.GetPushUrl(Request.Url, Request.ApplicationPath) %> {apikey}</strong>
            </blockquote>
            <p>Note: Current nuget server is in beta and only for testing. You do not required any API key to publish nuget packages.</p>            
        </fieldset>
        <br />
        <br />

        <fieldset style="width:800px">
            <legend><strong>Tutorial</strong></legend>
            To learn how to build and publish packages <a href="http://docs.nuget.org/docs/creating-packages/creating-and-publishing-a-package">visit the docs</a>.
        </fieldset>
        <% if (Request.IsLocal) { %>
        <p style="font-size:1.1em">
            To add packages to the feed put package files (.nupkg files) in the folder "<% = NuGet.Server.Infrastructure.PackageUtility.PackagePhysicalPath%>".
        </p>
        <% } %>
    </div>
</body>
</html>
