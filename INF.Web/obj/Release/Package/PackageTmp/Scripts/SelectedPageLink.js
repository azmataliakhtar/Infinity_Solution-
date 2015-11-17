var links_update_loaded = false
var mainMenuLinkClass = "templatemo_menu";
var mainMenuSelectedClass = "current";
var mainSubTabClass = "navstyle";
var mainSubTabSelectedClass = "navactive";

var mainFooterClass = "a8";
var mainFooterSelectedClass = "a8selected";

function updatePageFooterLinks(selectedObject, location)
{
    setObjectClass("lnkFooterHome", mainFooterClass)
    setObjectClass("lnkFooterAbout", mainFooterClass)
    setObjectClass("lnkFooterHowItWorks", mainFooterClass)
    setObjectClass("lnkFooterPrivacy", mainFooterClass)
    setObjectClass("lnkFooterFAQs", mainFooterClass)
    setObjectClass("lnkFooterContactUs", mainFooterClass)
    
    var footerLink = "";
    if(!selectedObject)
    {
        selectedObject = "";
    }
    
    selectedObject = selectedObject.toLowerCase();

    switch(selectedObject)
    {
        case "lnkabout":
            footerLink = "lnkFooterAbout";
            break;

        case "lnkhowitworks":
            footerLink = "lnkFooterHowItWorks";
            break;

        case "privacypolicy.html":
            footerLink = "lnkFooterPrivacy";
            break;

        case "faqs.html":
            footerLink = "lnkFooterFAQs";
            break;

        case "contactus.html":
            footerLink = "lnkFooterContactUs";
            break;
        
        default:
            if(location.indexOf("/public/default.aspx") > 0)
            {
                footerLink = "lnkFooterHome";
            }
            break;
    }
    
    if(footerLink != "")
    {
        setObjectClass(footerLink, mainFooterSelectedClass, true)
    }
}

function updatePageSubTabLinks(profilePageOrder) {    
    setObjectClass("lnkTabUserSettings", mainSubTabClass)
    setObjectClass("lnkTabProfilePersonal", mainSubTabClass)
    setObjectClass("lnkTabProfileCompany", mainSubTabClass)
    setObjectClass("lnkTabProfileDepartment", mainSubTabClass)
    
    if(profilePageOrder == -1)//No profile page is selected
    {
        setObjectClass("lnkTabUserSettings", mainSubTabSelectedClass)
        removeLinkFromTab("lnkTabUserSettings");
    }
    else if(profilePageOrder == 1)//Personal Profile
    {
        setObjectClass("lnkTabProfilePersonal", mainSubTabSelectedClass)
        removeLinkFromTab("lnkTabProfilePersonal");
    }
    else if(profilePageOrder == 2)//Company Profile
    {
        setObjectClass("lnkTabProfileCompany", mainSubTabSelectedClass)
        removeLinkFromTab("lnkTabProfileCompany");
    }
    else if((profilePageOrder == 3) || (profilePageOrder == 4))//Department Profile
    {
        setObjectClass("lnkTabProfileDepartment", mainSubTabSelectedClass)
        removeLinkFromTab("lnkTabProfileDepartment");
    }
}

function updatePageLinks(selectedObject, forceLoad) {
    //debugger;
    if((links_update_loaded) && (!forceLoad))
    {
        return;
    }
    
    links_update_loaded = true;
    otherSelectedObject = "";

    setObjectClass("ctl00_liHome", mainMenuLinkClass)
    setObjectClass("liHome", mainMenuLinkClass)

    setObjectClass("ctl00_liMealDeals", mainMenuLinkClass)
    setObjectClass("liMealDeals", mainMenuLinkClass)

    setObjectClass("ctl00_liMenu", mainMenuLinkClass)
    setObjectClass("liMenu", mainMenuLinkClass)

    setObjectClass("ctl00_liTrackOrder", mainMenuLinkClass)
    setObjectClass("liTrackOrder", mainMenuLinkClass)

    setObjectClass("ctl00_liFeedback", mainMenuLinkClass)
    setObjectClass("liFeedback", mainMenuLinkClass)

    setObjectClass("ctl00_liSignIn", mainMenuLinkClass)
    setObjectClass("liSignIn", mainMenuLinkClass)
    
    var location = document.location.href;
    location = location.toLowerCase();
    
    if((location.indexOf("default.aspx") > 0) || (location.indexOf(".aspx") < 0))
    {
        selectedObject = "ctl00_liHome";        
    }
    else if(location.indexOf("mealdeals.aspx") > 0)
    {
        selectedObject = "ctl00_liMealDeals";        
    }
    else if(location.indexOf("ourmenu.aspx") > 0)
    {
        selectedObject = "ctl00_liMenu"; 
    }
    else if ((location.indexOf("trackorder.aspx") > 0))
    {
        selectedObject = "ctl00_liTrackOrder"; 
    }
    else if (location.indexOf("feedback.aspx") > 0)
    {
        selectedObject = "ctl00_liFeedback"
    }
    else if(location.indexOf("signup.aspx") > 0)
    {
        selectedObject = "ctl00_liSignIn"
    }
        
    if(selectedObject)
    {
        setObjectClass(selectedObject, mainMenuSelectedClass)
        setObjectClass("ctl00_" + selectedObject, mainMenuSelectedClass)
    }

    if (otherSelectedObject != "")
    {
        setObjectClass(otherSelectedObject, mainMenuSelectedClass)
        setObjectClass("ctl00_" + otherSelectedObject, mainMenuSelectedClass)
    }

    //updatePageFooterLinks(selectedObject, location);
}

function setObjectClass(id, cssClass, reLoad)
{
    var lnk = document.getElementById(id);
    
    if(lnk)
    {
        lnk.className = cssClass;
    }
    else
    {
        if(reLoad)
        {
            var code = "setObjectClass('"+id+"', '"+cssClass+"', "+true+")";
            setTimeout(code, 500);
        }
    }
}

/***********Usman*********/
var currentExpandItem = null;
var currentExpandItemImg = null;

function onExpandList(trDescription, imgExpand, itemID) {
    //debugger;
    var objDescription = document.getElementById(trDescription + itemID);
    var objImgExpan = document.getElementById(imgExpand + itemID);
    var tdCategoryName = document.getElementById("tdCategoryName");
    //debugger;
    if (tdCategoryName) {
        tdCategoryName.className = "baractive";
    }

    if (!objDescription) {
        return;
    }

    if (objDescription.style.display != "none") {
        objDescription.style.display = "none";
        //objImgExpan.src = currentExpandItemImg.src.replace("arrow_up.gif", "arrow_down.gif");
        currentExpandItem = null;
        currentExpandItemImg = null;
        return;
    }

    if (currentExpandItem) {
        currentExpandItem.style.display = "none";
        //currentExpandItemImg.src = currentExpandItemImg.src.replace("arrow_up.gif", "arrow_down.gif");
    }

    currentExpandItem = objDescription;
    currentExpandItemImg = objImgExpan;

    if (objDescription) {
        objDescription.style.display = "";
        //objImgExpan.src = objImgExpan.src.replace("arrow_down.gif", "arrow_up.gif");
    }
}
/***********Usman*********/