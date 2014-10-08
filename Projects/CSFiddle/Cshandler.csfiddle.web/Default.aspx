<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Cshandler.csfiddle.web.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>csfiddlecode.net</title>
    <style type="text/css">
        textarea {
            overflow-y: hidden; /* prevents scroll bar flash */
            padding-top: 1.1em; /* prevents text jump on Enter keypress */
            width: 900px;
            height: 400px;
        }
    </style>
    <meta name="viewport" content="width=device-width" />
    <script src="Scripts/jquery-1.10.2.min.js"></script>
 <%--   <script src="Scripts/keywords.js"></script>--%>
    <script src="Scripts/jquery.snippet.min.js"></script>
    <link href="Scripts/jquery.snippet.min.css" rel="stylesheet" />
    
<%--    <script type="text/javascript" src="Scripts/shCore.js"></script>
	<script type="text/javascript" src="Scripts/shBrushCSharp.js"></script>
	<link type="text/css" rel="stylesheet" href="Styles/shCoreDefault.css"/>--%>
<%--	<script type="text/javascript">SyntaxHighlighter.all();</script>--%>

    <script type="text/javascript">

        $(document).ready(function () {

            //$("pre.csharp").snippet("csharp", {
            //    style: "typical",
            //    //clipboard: "Scripts/ZeroClipboard.swf",
            //    showNum: false
            //});
            
            $("pre.dynamic").snippet({ style: "vampire", transparent: true, showNum: false });

            $("pre#dynamic").snippet("php", { style: "navy", showNum: false });


            $('#<%=txtSourceLib.ClientID%>').keyup(setup);
            $('#<%=txtMain.ClientID%>').keyup(setupMainPre);
            $('#<%=txtMain.ClientID%>').bind("load", setupMainPre());
            
            setup();
        });

        function setup() {
            $('#srclib').html($('#<%=txtSourceLib.ClientID%>').val());
            $('#srclib').removeClass("sh_csharp snippet-formatted sh_sourceCode");

            var pre = $('#srclib');
            $("#divsrclib").children().remove();
            $("#divsrclib").append(pre);
            
            $("pre#srclib").snippet("csharp", {
                style: "typical",
                clipboard: "Scripts/ZeroClipboard.swf",
                showNum: false,
            });
        }


        function setupMainPre() {

            $('#srcmain').html($('#<%=txtMain.ClientID%>').val());
            $('#srcmain').removeClass("sh_csharp snippet-formatted sh_sourceCode");

            var pre = $('#srcmain');
            $("#divmain").children().remove();
            $("#divmain").append(pre);

            $("pre#srcmain").snippet("csharp", {
                style: "typical",
                clipboard: "Scripts/ZeroClipboard.swf",
                showNum: false,
            });
        }
        
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
                            <div style="color:lightblue; max-width: 100%; text-align: left">Write your library functions here</div>
                            <asp:TextBox ID="txtSourceLib" TextMode="MultiLine" Width="900px" Height="400px" runat="server">
                            </asp:TextBox>
                        </td>
                        <td valign="top">
                             <div id="divsrclib">
                                <pre id="srclib" class="csharp">
                                <%--<span id="srclib"></span>--%>
                                </pre>
                            </div>
                        </td>
                    </tr>
                    <tr>
                    <td valign="top"><span style="color:greenyellow" >Main:</span></td>
                        <td valign="top">
                            <div style="color:lightblue; max-width: 100%; text-align: left">use <b>Print()</b> statement in Main to write output.</div>
                            <asp:TextBox ID="txtMain" TextMode="MultiLine" Width="900px" Height="200px" runat="server">
                            </asp:TextBox>
                        </td>
                        <td valign="top">
                             <div id="divmain">
                                <pre id="srcmain" class="csharp">
                                <%--<span id="srclib"></span>--%>
                                </pre>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="float:none">
                <asp:Button Text="Compile and Run" ID="Button1" runat="server" OnClick="btnCompileAndRun_Click" />
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

<%--    <meta name="viewport" content="width=device-width, user-scalable=false;">
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="color:lightblue; max-width: 100%; text-align: right">use <b>Print()</b> statement in Main to write output.</div>
            <div style="float: right">
                <table>
                    <tr>
                        <td valign="top"><span style="color:greenyellow">Library:</span> 
                        </td>
                        <td>
                            <asp:TextBox ID="txtSourceLib" TextMode="MultiLine" Height="900px" Width="500px" runat="server">
                            </asp:TextBox>
                        </td>
                        <td valign="top"><span style="color:greenyellow" >Main:</span></td>
                        <td>
                            <asp:TextBox ID="txtMain" TextMode="MultiLine" Height="900px" Width="500px" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="float:none">
                <asp:Button Text="Compile and Run" ID="btnCompileAndRun" runat="server" OnClick="btnCompileAndRun_Click" />
                <br/>
                <b>Output:</b>
                <br />
                <asp:Label ID="lblOutput" Text="" runat="server" />
            </div>
        </div>
    </form>
                
            $('#<%=txtMain.ClientID%>').on('keyup', 'textarea', function (e) {
                $(this).css('height', 'auto');
                $(this).height(this.scrollHeight);
            });

            $('#<%=txtMain.ClientID%>').keyup();

            $('#<%=txtSourceLib.ClientID%>').on('keyup', 'textarea', function (e) {
                $(this).css('height', 'auto');
                $(this).height(this.scrollHeight);
            });
            
            $('#<%=txtSourceLib.ClientID%>').keyup();
    --%>