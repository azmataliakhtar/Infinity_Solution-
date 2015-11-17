



function addExtraLabels()
{
    $(document).find('.prodName').each(function()
    {
        var getText = $(this).html();

        if(getText.indexOf('[hot]') > 0)
        {
            var tempHot = "<span class='hotIcon'>&nbsp;</span>";
            getText = getText.replace('[hot]', tempHot);
        }

        if(getText.indexOf('[spicy]') > 0)
        {
            var tempHot = "<span class='spicyIcon'>&nbsp;</span>";
            getText = getText.replace('[spicy]', tempHot);
        }

        if(getText.indexOf('[mild]') > 0)
        {
            var tempHot = "<span class='mildIcon'>&nbsp;</span>";
            getText = getText.replace('[mild]', tempHot);
        }

        if(getText.indexOf('[veg]') > 0)
        {
            var tempHot = "<span class='vegIcon'>&nbsp;</span>";
            getText = getText.replace('[veg]', tempHot);
        }


       $(this).html(getText);
        

    });
}


function removeExtraLabels(element)
{
    var getTempText = $(element).html();
    getTempText = getTempText.replace('[hot]', '').replace('[spicy]', '').replace('[mild]', '').replace('[veg]', '');
    $(element).html(getTempText);

   // console.log('Removed extra label');
}

$(document).ready(function () {


    addExtraLabels();

      $('#menunavbarMobi .navbar-brand').on('click', function (e) {
            $(this).parent().find(".navbar-toggle").click();
      });



    //$('.basketMobi').fadeOut();

    if (window.location.href.indexOf("Menu.aspx") > -1 || window.location.href.indexOf("menu.aspx") > -1 || window.location.href.indexOf("Deals.aspx") > -1 || window.location.href.indexOf("deals.aspx") > -1 ) {
        $('.basketMobi').fadeIn();
        $('.headerRowMobi').addClass('marginTopMobi');
    }
    else {
        $('.basketMobi').fadeOut();
        $('.headerRowMobi').removeClass('marginTopMobi');
    }

    $('.testBasket').click(function(){
        $.fancybox({
            'content' : $(".shopping_cart").html()
        });
    });

        
    $('.basketMobi').click(function () {

       // alert('scroll to basket !');

       // $(window).scrollTo('#sidebar');

        $('html,body').animate({
            scrollTop: $("#" + "sidebar").offset().top
        },
        'fast');

    });
 

    if (window.location.href.indexOf("Default.aspx") > -1 || window.location.href.indexOf("default.aspx") > -1) {

        $('.liHome').parent().addClass('selected');
    }
    else if (window.location.href.indexOf("Menu.aspx") > -1 || window.location.href.indexOf("menu.aspx") > -1) {
        $('.liMenu').parent().addClass('selected');

        
        var checkordertype = $('.orderOrCollP').html();

        if (checkordertype.indexOf("Order Service") > -1) {

           

            $('#ctl00_ucHeader_pnlOrderTypeAndPostCode').fadeIn();
        }
        else {
            $('#overlay').fadeIn(300, function () {
                $('#orderselection').fadeIn(400);
            });
        }
        

       // $('.basketMobi').fadeIn();


    }
    else if (window.location.href.indexOf("Deals.aspx") > -1 || window.location.href.indexOf("deals.aspx") > -1) {
        $('.liDeals').parent().addClass('selected');
        
        var checkordertype = $('.orderOrCollP').html();
        if (checkordertype.indexOf("Order Service") > -1) {
            $('#ctl00_ucHeader_pnlOrderTypeAndPostCode').fadeIn();
        }
        else {
            $('#overlay').fadeIn(300, function () {
                $('#orderselection').fadeIn(400);
            });
        }
        


    }
    else if (window.location.href.indexOf("TrackOrder.aspx") > -1 || window.location.href.indexOf("track") > -1) {
        $('.liTrackOrder').parent().addClass('selected');
    }
    else if (window.location.href.indexOf("Reservation.aspx") > -1 || window.location.href.indexOf("reservation.aspx") > -1) {
        $('.liReservation').parent().addClass('selected');
    }
    else if (window.location.href.indexOf("PhotoGallery.aspx") > -1 || window.location.href.indexOf("allery.aspx") > -1) {
        $('.liPhotoGallery').parent().addClass('selected');
    }
    else if (window.location.href.indexOf("Testimonials.aspx") > -1 || window.location.href.indexOf("testimonials.aspx") > -1) {
        $('.liTestimonials').parent().addClass('selected');
    }
    else if (window.location.href.indexOf("ContactUs.aspx") > -1 || window.location.href.indexOf("contactus.aspx") > -1) {
        $('.liAboutUs').parent().addClass('selected');
    }
    else if (window.location.href.indexOf("Login.aspx") > -1 || window.location.href.indexOf("login.aspx") > -1) {
        $('.loginElement').parent().addClass('selected');
    }


    $('#facekDelBtn').click(function () {

        var getPostcode = $('#pcode').val();
        if (getPostcode.length > 1) {


            $('#searchPcode').click();
        } else {

            $('.pstcderr').fadeIn();

            setTimeout(function () {
                $('.pstcderr').fadeOut();
            }, 1500);


        }
    });


    

    $('#selectMonth').on('change', function (e) {
        var optionSelected = $("option:selected", this).text();
        if (optionSelected == "month") {
            $('.day29').css("display", "block");
            $('.day30').css("display", "block");
            $('.day31').css("display", "block");
        }
        else if (optionSelected == "February") {
            $('.day29').css("display", "none");
            $('.day30').css("display", "none");
            $('.day31').css("display", "none");
        }
        else if (optionSelected == "April") {
            $('.day29').css("display", "block");
            $('.day30').css("display", "block");
            $('.day31').css("display", "none");
        }
        else if (optionSelected == "June") {
            $('.day29').css("display", "block");
            $('.day30').css("display", "block");
            $('.day31').css("display", "none");
        }
        else if (optionSelected == "September") {
            $('.day29').css("display", "block");
            $('.day30').css("display", "block");
            $('.day31').css("display", "none");
        }
        else if (optionSelected == "November") {
            $('.day29').css("display", "block");
            $('.day30').css("display", "block");
            $('.day31').css("display", "none");
        }
        else {
            $('.day29').css("display", "block");
            $('.day30').css("display", "block");
            $('.day31').css("display", "block");
        }

        //alert(optionSelected);
    });



    /*
    $('.pseudoSettings').click(function () {

        // $('#ctl00_ucHeader_LinkButton1').click();

        $(this).parent().find('a').click();
        // WebForm_PostBackOptions("#ct100_ucHeader_LinkButton1", "", false, "", "Profile.aspx", false, true);
        //alert($(document).find('#ctl00_ucHeader_LinkButton1').attr("href"));
        // alert("settings click working");

    })
    */

    function showReservationError() {
        $('.errorLine').slideDown();

        setTimeout(function () {
            $('.errorLine').slideUp();
        }, 1500);

    }

    $('#sendReservationForm').click(function () {

        // sendReservation();

        var xEmail = $('#bookingEmail').val();
        var xPhone = $('#bookingPhone').val();
        var xFirstName = $('#bookingFirstName').val();
        var xLastName = $('#bookingLastName').val();

        var xPeople = $('#selectPeople').find(":selected").text();
        var xMonth = $('#selectMonth').find(":selected").text();
        var xDay = $('#selectbxDay').find(":selected").text();

        var xHour = $('#selHour').find(":selected").text();
        var xMin = $('#selMin').find(":selected").text();
        var xAMPM = $('#selAMPM').find(":selected").text();

        var xComments = $('#additInfoTxtarea').val();

        if (xEmail.length < 1 || xPhone.length < 1 || xFirstName.length < 1 || xLastName.length < 1) {
            showReservationError();
        }
        else {

            if (xMonth == "month") {
                alert("Please choose a Month");
            }
            else {
                if (xDay == "day") {
                    alert("Please choose a Day");
                }
                else {
                    if (xHour == "hour" || xMin == "min") {
                        alert("Please choose the time when you will arrive (Hour and Minutes).");
                    }
                    else {


                        //var buildMsg = "Persons: " + xPeople + ", Month: " + xMonth + ", Day: " + xDay + ", Time: " + xHour + ":" + xMin + " " + xAMPM;

                        if (xComments.length < 1) {
                            xComments = "";
                        } else {
                            xComments = "Comments: " + xComments + "";
                        }

                        var xDateSummed = xDay + " " + xMonth;
                        var xTimeSummed = xHour + ":" + xMin + " " + xAMPM;
                        sendReservation(xEmail, xPhone, xFirstName, xLastName, xPeople, xDateSummed, xTimeSummed, xComments)
                        //alert(xComments);
                    }

                }
            }
        }

    }); 
    
    

});


function sendReservation(email, phonenr, firstname, lastname, nrpeople, datereserv, datetime, comment) {


    $.post("/Ajax/SendReservation.aspx",
        {
            emailClient: email,
            phone: phonenr,
            firstName: firstname,
            lastName: lastname,
            nrPeople: parseInt(nrpeople),
            dateReservation: datereserv,
            dateTime: datetime,
            customerComment: comment,
        }, function (result) {

            // alert(result);

            if (result == "Success") {

                //$('#reservationSuccess').fadeIn();

                /*
                setTimeout(function () {
                    $('#reservationSuccess').fadeOut();
                    location.reload;
                }, 1500);
                */

                $.fancybox({
                    'content': $("#reservationSuccess").html()
                });

                //location.reload
                //alert("We have received")
            }

            return false;
        });

}



$(window).load(function () {
     $('#featured').orbit();
});

