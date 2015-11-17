<%@ page title="" language="VB" masterpagefile="~/SiteMaster.master" autoeventwireup="false" Inherits="INF.Web.Catering" Codebehind="Catering.aspx.vb" %>
<%@ Import Namespace="INF.Web.UI.Settings" %>

<asp:Content ID="GoogleContent" ContentPlaceHolderID="GooglePlaceHolder" runat="Server">
        
      <%
          If (ConfigurationManager.AppSettings("google-site-verification") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("google-plus-link")))) Then
        %>                                
             <meta name="google-site-verification" content="<% Response.Write(ConfigurationManager.AppSettings("google-site-verification"))%>"/>
        <%
        End If
    %>    
    
     <%
         If (ConfigurationManager.AppSettings("google-plus-link") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("google-plus-link")))) Then
        %>                                
             <link href="<% Response.Write(ConfigurationManager.AppSettings("google-plus-link"))%>" rel="publisher"/>
        <%
        End If
    %>   
   
     <%
         If (ConfigurationManager.AppSettings("geo-region") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("geo-region")))) Then
        %>                                
             <meta name="geo.region" content="<% Response.Write(ConfigurationManager.AppSettings("geo-region"))%>" />
        <%
        End If
    %> 

    <%
        If (ConfigurationManager.AppSettings("geo-placename") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("geo-placename")))) Then
        %>                                
             <meta name="geo.placename" content="<% Response.Write(ConfigurationManager.AppSettings("geo-placename"))%>" />
        <%
        End If
    %> 


     <%
         If (ConfigurationManager.AppSettings("geo-position") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("geo-position")))) Then
        %>                                
             <meta name="geo.position" content="<% Response.Write(ConfigurationManager.AppSettings("geo-position"))%>" />
        <%
        End If
    %> 

     <%
         If (ConfigurationManager.AppSettings("google-icbm") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("google-icbm")))) Then
        %>                                
             <meta name="ICBM" content="<% Response.Write(ConfigurationManager.AppSettings("google-icbm"))%>" />
        <%
        End If
    %> 

</asp:Content>

<asp:Content runat="server" ID="PageTitleContent" ContentPlaceHolderID="PageTitle">
    <%
        If (ConfigurationManager.AppSettings("titleCatering") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("titleCatering")))) Then
        %>                                
            <%=ConfigurationManager.AppSettings("titleCatering")%>
        <%
        Else
            %> 
                <% Response.Write(EPATheme.Current.Themes.WebsiteName)%>  
            <%
        End If
    %>   
</asp:Content>

<asp:Content runat="server" ID="LinkCanonicalContent" ContentPlaceHolderID="LinkCanonical">

    <%
         If (ConfigurationManager.AppSettings("linkCanonicalCatering") IsNot Nothing AndAlso Not (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings("linkCanonicalCatering")))) Then
        %>                                
            <link rel="canonical" href="<%=ConfigurationManager.AppSettings("linkCanonicalCatering")%>" />
        <%
        End If
    %>   



</asp:Content>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadExtra" runat="Server">

</asp:Content>

<asp:Content ID="MainContentPlaceHolderContent" ContentPlaceHolderID="MainContent" runat="Server">

    <section class="catering cstmsection">
       
        <h3 class="page-header page-title"><i class="fa fa-birthday-cake"></i>Catering Services</h3>
            
                         
            <div class="cateringShow">

                    <div class="aboutusImg">
                       <img src="App_Themes/default/imgs/catering.jpg" />
                    </div>  


                    <div class="cateringText">  
                                          			
                        <h2>Let us bring Our Cuisine experience to you</h2>
                        <p>Our reputation for fine outdoor catering services has grown over the years through word of mouth recommendation; this is because we only love what we do. Our philosophy can be explained in few words: outstanding food, exceptional service and personal approach to party. Whether you plan an intimate dinner or a large scale party we will relish the challenge of providing an exceptional food service.</p>
  
                       
                    </div>
                    
                    <div style="float: left; margin: 10px 1%; width: 98%;">
	                        <ul class="ice-check2">
		                        <li>
			                        <span style="color:#CC0000;">Corporate functions</span></li>
		                        <li>
			                        <span style="color:#CC0000;">Launches &amp; openings</span></li>
		                        <li>
			                        <span style="color:#CC0000;">Cocktail parties</span></li>
		                        <li>
			                        <span style="color:#CC0000;">Formal receptions</span></li>
		                        <li>
			                        <span style="color:#CC0000;">Weddings</span></li>
		                        <li>
			                        <span style="color:#CC0000;">Charity functions</span></li>
		                        <li>
			                        <span style="color:#CC0000;">Garden &amp; house parties</span></li>
		                        <li>
			                        <span style="color:#CC0000;">Birthday &amp; Anniversary parties</span></li>
		                        <li>
			                        <span style="color:#CC0000;">Outdoor events</span></li>
	                        </ul>
                        </div>

             </div>


            
    </section>


   
   

</asp:Content>
