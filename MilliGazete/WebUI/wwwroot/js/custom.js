
//Scroll Top
var topBtn = $('#buttonTopScroll');

$(window).scroll(function() {
  if ($(window).scrollTop() > 300) {
    topBtn.addClass('show');
  } else {
    topBtn.removeClass('show');
  }
});

topBtn.on('click', function(e) {
  e.preventDefault();
  $('html, body').animate({scrollTop:0}, '300');
});

//Toogle Big Menu
function toogleMenu() {
    if ($('#bigMenu').is(":hidden")) {
        $('#bigMenu').show('slow');
        $('#iconBigMenu').removeClass('fa-bars').addClass('fa-times');
    } else {
        $('#bigMenu').hide('slow');
        $('#iconBigMenu').removeClass('fa-times').addClass('fa-bars');
    }
}

