$(document).mouseup(function(e){
	var container = $(".top-search-field");
	// If the target of the click isn't the container
	if(!container.is(e.target) && container.has(e.target).length === 0){
		container.hide();
	}
});

$(document).ready(function(){

	/* init tooltips and popovers */
	$('[data-toggle="tooltip"]').tooltip(); 
	$('[data-toggle="popover"]').popover();

	
	/*mainnav dropdown active on hover*/
	$(".dropdown.main-drop").hover(            
		function() {
			$('.dropdown-menu', this).not('.in .dropdown-menu').stop(true,true).fadeIn(120);
			$(this).addClass('open');
		},
		function() {
			$('.dropdown-menu', this).not('.in .dropdown-menu').stop(true,true).fadeOut(120);
			$(this).removeClass('open');
		}
	);
	/* default button link active on hover */
	$('.dropdown-toggle.main-drop-toggle').click(function(e) {
	  /*if ($(document).width() > 768) {*/
		e.preventDefault();
		var url = $(this).attr('href');
		if (url !== '#') {
		  window.location.href = url;
		}
	 /* }*/
	});
	

	/*mobile menu init*/
	$('.mobile-menu-toggler').sidr({
		displace: false,
		name: 'mobile-menu',
		side: 'left',
		onOpen: function(name) {
			$('html, body').css({'overflow': 'hidden'});
			$('#mobile-overlay').addClass("mobile-overlay-open");
		},
		onClose: function(name) {
			$('html, body').css({'overflow': 'auto'});
			$('#mobile-overlay').removeClass("mobile-overlay-open");
		}			
	});
	/*mobile closers*/
	$('#mobile-overlay, #mobile-menu-close').click(function () {
		$.sidr('close', 'mobile-menu');
	});

		
		
	/* page up script */
	var scrollTimeout;  // global for any pending scrollTimeout
	
	$(window).scroll(function () {
		if (scrollTimeout) {
			// clear the timeout, if one is pending
			clearTimeout(scrollTimeout);
			scrollTimeout = null;
		}
		scrollTimeout = setTimeout(scrollHandler, 250);
	});
	
	scrollHandler = function () {
		// Check your page position
		if ($(window).scrollTop() > 200) {
			$('.go-top').fadeIn(200);
		} else {
			$('.go-top').fadeOut(200);
		}
	};
	
	$('.go-top').click(
		function (event) { 
			event.preventDefault(); 
			$('html, body').animate({ scrollTop: 0 }, 300);
		}
	);


	/*open a tab*/
	function activaTab(tab){
	  $('.nav-tabs a[href="#' + tab + '"]').tab('show');
	};

	/* product list xs view filter but*/
	$('.btn-plist-filter').click(
		function (event) { 
			event.preventDefault(); 
	
			var filtermodalstatus = $('body').hasClass('filter-open');
	
			if (filtermodalstatus){
				$('body').removeClass('filter-open');
				$('.filters-wrapper').hide();this.blur();
			} else {
				$('body').addClass('filter-open'); /* prevent body scroll when phone size */
				$('.filters-wrapper').show();this.blur();
			}
		}
	);
	
	/* enable scroll if resized to larger than mobile --- this fix is replaced with a new function at bottom*/
	/*	window.onresize = function (event) {
		if (window.innerWidth >= 768) {
			$('body').removeClass('filter-open');
		}
	};*/


	/* sliders */


	$('.surmanset-slide').slick({
		slidesToShow: 4,
        slidesToScroll: 1,
		arrows: false,
		infinite: false,
        dots: false,
		  responsive: [
			{
			  breakpoint: 992,
			  settings: {
				arrows:true,
				slidesToShow: 3,
				slidesToScroll: 1}
			},
			{
			  breakpoint: 680,
			  settings: {
				arrows:true,
				slidesToShow: 2,
				slidesToScroll: 1
				}
			},
			{
			  breakpoint: 460,
			  settings: {
				infinite: true,
				centerMode: true,
		        centerPadding: '50px',
				arrows:true,
				slidesToShow: 1,
				slidesToScroll: 1}
			}
		  ]
      });	


	  $('.large-slide').on('init', function(event, slick){/* add show all button to large slide */
			$('.large-slide .slick-dots').append('<a href="/" class="large-slide-all-button"><i class="fa fa-ellipsis-h" aria-hidden="true"></i></a>');
	  });
	  
	  $('.large-slide').slick({
        slidesToShow: 1,
		slidesToScroll: 1,
		arrows: false,
		infinite: true,
        dots: true,
		autoplay: true,
		speed: 200,
		autoplaySpeed: 5000,
		responsive: [
			{
			  breakpoint: 992,
			  settings: {
				slidesToShow: 1,
				slidesToScroll: 1}
			},
			{
			  breakpoint: 600, /* < 600 */
			  lazyLoad: "progressive",
			  settings: {
				slidesToShow: 1,
				slidesToScroll: 1}
			}
		]
      });

	  
	  $('.small-slide').slick({
        slidesToShow: 1,
		slidesToScroll: 1,
		arrows: false,
		infinite: true,
        dots: true,
		speed: 200,
		responsive: [
			{
			  breakpoint: 992,
			  settings: {
				slidesToShow: 1,
				slidesToScroll: 1}
			},
			{
			  breakpoint: 600, /* < 600 */
			  lazyLoad: "progressive",
			  settings: {
				slidesToShow: 1,
				slidesToScroll: 1}
			}
		]
      });	
	
	
	  /* slide dots work with hover function */
	  $('.slick-dots li').on('mouseover', function() {
	    $(this).parents('.slick-slider').slick('goTo', $(this).index());
	  });
	  
	  /* photogallery slide */
	 $('.photogallery').slick({
	  slidesToShow: 1,
	  slidesToScroll: 1,
	  arrows: true,
	  fade: true,
	  asNavFor: '.photogallery-nav',
	  responsive: [
			{
			  breakpoint: 500, /* < 500 */
			  lazyLoad: "progressive",
			  settings: {
				arrows: false
				}
			}
		]
	});
	$('.photogallery-nav').slick({
	  slidesToShow: 5,
	  slidesToScroll: 1,
	  asNavFor: '.photogallery',
	  dots: false,
	  arrows: false,
	  centerMode: true,
	  focusOnSelect: true,
	  responsive: [
			{
			  breakpoint: 600, /* < 600 */
			  lazyLoad: "progressive",
			  settings: {
				slidesToShow: 3,
				slidesToScroll: 1}
			}
		]
	});
	  
	  
	$('.doublerow-slide').slick({
		rows: 3,
		slidesPerRow: 4,
		/*slidesToShow: 1,
        slidesToScroll: 1,*/
		arrows: false,
		infinite: false,
        dots: false,
		  responsive: [
			{
			  breakpoint: 992,
			  settings: {
				dots:true,
				rows: 2,
				slidesPerRow: 3/*,
				slidesToShow: 1,
				slidesToScroll: 1*/}
			},
			{
			  breakpoint: 680,
			  settings: {
				dots:true,
				rows: 1,
				slidesPerRow: 2/*,
				slidesToShow: 1,
				slidesToScroll: 1*/
				}
			},
			{
			  breakpoint: 460,
			  settings: {
				arrows:true,
				dots:true,
				rows: 1,
				slidesPerRow: 1/*,
				slidesToShow: 1,
				slidesToScroll: 1*/}
			}
		  ]
      });
	
});

/* ***Other helper functions*** */

/* horizontal scroll check jquery plug-in for responsive conditions */
	(function($) {
		$.fn.hasHrzScrollBar = function() {
			return this.get(0).scrollWidth > this.width();
		}
	}(jQuery));



// media query event handler for mobile functions
	if (matchMedia) {
		const mq = window.matchMedia("(min-width: 768px)");
		mq.addListener(WidthChange);
		WidthChange(mq);
	}

	// media query change
	function WidthChange(mq) {
		if (mq.matches) {
			// window width is at least 768px
			/*$('.top-search-field').show();*//* making sure mobile search bar is shown after resize to desktop */
			$('body').removeClass('filter-open');/*if left prevents scroll on normal sizes.*/
		} else {
			// window width is less than 768px
			$('.top-search-field').hide();/* making sure mobile search bar is hidden after resize to mobile */
			
		/*mobile topnav hide effect*/
			var scrollTimeOut = true,
				lastYPos = 0,
				yPos = 0,
				yPosDelta = 20,
				nav = $('#header'),
				navHeight = nav.outerHeight(),
				setNavClass = function() {
					scrollTimeOut = false;
					yPos = $(window).scrollTop();
			
					if(Math.abs(lastYPos - yPos) >= yPosDelta) {
						if (yPos > lastYPos && yPos > navHeight){
							nav.addClass('hide-nav');
						} else {
							nav.removeClass('hide-nav');
						}
						lastYPos = yPos;
					}
				};
			
			$(window).scroll(function(e){
				scrollTimeOut = true;
			});
			
			setInterval(function() {
				if (scrollTimeOut) {
					setNavClass();
				}
			
			}, 250);		
		/*mobile topnav hide effect end*/
			
		}
	}
// media query event handler for mobile functions end


/* disable ios input zoom effect */

	var iOS = navigator.platform && /iPad|iPhone|iPod/.test(navigator.platform)
	if (iOS)
	  preventZoomOnFocus();
	
	function preventZoomOnFocus()
	{
	  document.documentElement.addEventListener("touchstart", onTouchStart);
	  document.documentElement.addEventListener("focusin", onFocusIn);
	}
	
	let dont_disable_for = ["checkbox", "radio", "file", "button", "image", "submit", "reset", "hidden"];
	
	function onTouchStart(evt)
	{
	  let tn = evt.target.tagName;
	
	  // No need to do anything if the initial target isn't a known element
	  // which will cause a zoom upon receiving focus
	  if (    tn != "SELECT"
		  &&  tn != "TEXTAREA"
		  && (tn != "INPUT" || dont_disable_for.indexOf(evt.target.getAttribute("type")) > -1)
		 )
		return;
	
	  // disable zoom
	  setViewport("width=device-width, initial-scale=1.0, user-scalable=0");
	}
	
	// NOTE: for now assuming this focusIn is caused by user interaction
	function onFocusIn(evt)
	{
	  // reenable zoom
	  setViewport("width=device-width, initial-scale=1.0, user-scalable=1");
	}
	
	// add or update the <meta name="viewport"> element
	function setViewport(newvalue)
	{
	  let vpnode = document.documentElement.querySelector('head meta[name="viewport"]');
	  if (vpnode)
		vpnode.setAttribute("content",newvalue);
	  else
	  {
		vpnode = document.createElement("meta");
		vpnode.setAttribute("name", "viewport");
		vpnode.setAttribute("content", newvalue);
	  }
	}
	
/* disable ios input zoom effect end */

/* font size change */
$(document).ready(function(){        
	var originalSize = $('.news-container-text').css('font-size');
	// reset
	$(".resetMe").click(function(){
		$('.news-container-text').css('font-size', originalSize);
	});

	// Increase Font Size
	$(".news-button.textsize.t-up").click(function(){
		var currentSize = $('.news-container-text').css('font-size');
		var currentSize = parseFloat(currentSize)+3;
		$('.news-container-text').css('font-size', currentSize);
		return false;
	});

	// Decrease Font Size       
	$(".news-button.textsize.t-down").click(function(){
		var currentFontSize = $('.news-container-text').css('font-size');
		var currentSize = $('.news-container-text').css('font-size');
		var currentSize = parseFloat(currentSize)-3;
		$('.news-container-text').css('font-size', currentSize);
		return false;
	});
});
