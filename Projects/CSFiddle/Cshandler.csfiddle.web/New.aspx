<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="New.aspx.cs" Inherits="Cshandler.csfiddle.web.New" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>csfiddlecode.net</title>
    <meta name="viewport" content="width=device-width">
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script src="Scripts/keywords.js"></script>
    <script src="Scripts/jquery.snippet.min.js"></script>
    <link href="Scripts/jquery.snippet.min.css" rel="stylesheet" />
    
    <script type="text/javascript" src="Scripts/shCore.js"></script>
	<script type="text/javascript" src="Scripts/shBrushCSharp.js"></script>
	<link type="text/css" rel="stylesheet" href="Styles/shCoreDefault.css"/>
	<script type="text/javascript">SyntaxHighlighter.all();</script>

    <script type="text/javascript">
        
        $(document).ready(function () {
            //$("pre.csharp").snippet("csharp", {
            //    style: "typical",
            //    //clipboard: "Scripts/ZeroClipboard.swf",
            //    showNum: false
            //});
            //$("pre.dynamic").snippet({ style: "vampire", transparent: true, showNum: false });
            
            $("pre#dynamic").snippet("php", { style: "navy", showNum: false });
        });

        //$("pre.csharp").click(function() {
        //    $("pre.csharp").snippet("csharp", {
        //        style: "typical",
        //        //clipboard: "Scripts/ZeroClipboard.swf",
        //        showNum: false
        //    });
        //});
        <%--<pre class="csharp">--%> 
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <table>
                    <tr>
                        <td valign="top"><span style="color:greenyellow">Library:</span> 
                        </td>
                        <td valign="top">
                            <div id="txtSourceLib" runat="server" class="textarea" contenteditable>
                               <pre class="brush: csharp;">
using System; 
public class Test 
{ 
  public string SayHello()  
  { 
   return "Hi, WWW."; 
  } 
}
                                </pre>
                            </div>
                        </td>
                    </tr>
                    <tr>
                    <td valign="top"><span style="color:greenyellow" >Main:</span></td>
                        <td valign="top">
                            <div style="color:lightblue; max-width: 100%; text-align: left">use <b>Print()</b> statement in Main to write output.</div>
                            <div id="txtMain" class="textarea" runat="server" contenteditable>
                               <pre class="brush: csharp;">
Test test = new Test(); 
Print(test.SayHello()); 
Print("Special symbols are running here"); 
var i = 10; 
Print(i.ToString());   
                                </pre>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="float:none">
                <asp:Button Text="Compile and Run" ID="btnCompileAndRun" runat="server" OnClick="btnCompileAndRun_Click" />
                <br/>
                <b>Output:</b>
                <br />
                <pre id="dynamic">
                <asp:Label ID="lblOutput" Text="" runat="server" />
                 </pre>
            </div>
        </div>
    </form>
</body>
</html>