<%@ Control Language="vb" AutoEventWireup="false" Inherits="INF.Web.Public.default.DefaultPageUserControl" %>
<section>
	<div class="homeContainer">
	<a href="Menu.aspx"><img src="/public/pizzaperfect/images/web-background.png"></a>
	</div>
    </section>
<section>
	
	<article class="containerMobile">
	<div class="headerMobile"><img src="/public/pizzaperfect/images/headerMobile.jpg" /></div>
	<div class="logoContainer"><img src="/public/pizzaperfect/images/logoMobile.png" /></div>
	<div class="nameContainer"><img src="/public/pizzaperfect/images/nameMobile.png" /></div>
	
	<div class="wrapperMobile">
	    <h2>Contact Us</h2>
	    
	    <script
		src="http://maps.googleapis.com/maps/api/js?key=AIzaSyDY0kkJiTPVd2U7aTOAwhc9ySH6oHxOIYM&sensor=false">
		</script>
		
		<script>
		    function initialize() {
		        var mapProp = {
		            center: new google.maps.LatLng(52.390422, -2.003571),
		            zoom: 18,
		            mapTypeId: google.maps.MapTypeId.ROADMAP
		        };
		        var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
		    }

		    google.maps.event.addDomListener(window, 'load', initialize);
	    </script>
	    <div id="googleMap"></div>
	    <br><br>
	    <div class="clear"></div>
	    
		<p>T: 01214 530080</p>
		<p>Website: pizzaperfect.uk.com</p>
		<hr>
	    
	    <h2>Opening Times</h2>
		<p>Sun-thu: 12pm - 12am</p>
		<p>Fri-Sat: 12pm - 1am</p>
		<hr>
	    
	    <h2>About Us</h2>
		<p>A Delicious Pizza and Takeaway restaurant in Birmingham. Place Order Online for quick delivery or Call us over the phone 0121 453 0080.</p>
	</div>
	
	<div class="footer"> Thanks for visiting PIZZA PERFECT </div>

	</article>

    </section>
