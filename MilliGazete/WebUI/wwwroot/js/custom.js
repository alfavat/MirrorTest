//Init Document Ready
$(document).ready(function () {
    prayerTimeCountDown(34); //Istanbul
    $(".breaking-news-ticker").show(); //Breaking News
});

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

//Change City
function changeCity(_id, _city) {
    $('#dropdownMenuButton').html(_city.toUpperCase());
    prayerTimeCountDown(_id);
    $('#lblPrayerLink').attr("href",getPrayerCityUrl(_id));
}

//Get Base Url
function getBaseUrl() {
    if (baseWebUrl != null && baseWebUrl != "") {
        return baseWebUrl+"/";
    } else {
        return window.location.href.match(/^.*\//);
    }
}

//Prayer Time Countdown
var interval;
function prayerTimeCountDown(_idCity) {
    
    clearInterval(interval);
    $(".lblPrayerLabel").html("");
    $(".lblPrayerTime").html("...");
    $.ajax({
        type: "GET",
        url: getBaseUrl() + "namaz-vakti?sehir=" + _idCity,
        async: true,
        cache: false,
        dataType: "json",
        success: function (data) {
            if (data != null) {
                var value = data[0];
                
                var currentDate = new Date();
                var calcDate = new Date();
                var currentHour = currentDate.getHours();
                var currentMinute = currentDate.getMinutes();
                var currentTime = currentHour * 60 + currentMinute;
                var dawnTime = value.dawnTime.hours * 60 + value.dawnTime.minutes;
                var sunTime = value.sunTime.hours * 60 + value.sunTime.minutes;
                var noonPrayer = value.noonPrayer.hours * 60 + value.noonPrayer.minutes;
                var afternoonPrayer = value.afternoonPrayer.hours * 60 + value.afternoonPrayer.minutes;
                var eveningPrayer = value.eveningPrayer.hours * 60 + value.eveningPrayer.minutes;
                var nightPrayer = value.nightPrayer.hours * 60 + value.nightPrayer.minutes;

                if (dawnTime <= currentTime && sunTime >= currentTime) {
                    $('.lblPrayerLabel').html('Güneşe');
                    calcDate.setHours(value.sunTime.hours);
                    calcDate.setMinutes(value.sunTime.minutes);
                } else if (sunTime <= currentTime && noonPrayer >= currentTime) {
                    $('.lblPrayerLabel').html('Öğleye');
                    calcDate.setHours(value.noonPrayer.hours);
                    calcDate.setMinutes(value.noonPrayer.minutes);
                } else if (noonPrayer <= currentTime && afternoonPrayer >= currentTime) {
                    $('.lblPrayerLabel').html('İkindiye');
                    calcDate.setHours(value.afternoonPrayer.hours);
                    calcDate.setMinutes(value.afternoonPrayer.minutes);
                } else if (afternoonPrayer <= currentTime && eveningPrayer >= currentTime) {
                    $('.lblPrayerLabel').html('Akşama');
                    calcDate.setHours(value.eveningPrayer.hours);
                    calcDate.setMinutes(value.eveningPrayer.minutes);
                } else if (eveningPrayer <= currentTime && nightPrayer >= currentTime) {
                    $('.lblPrayerLabel').html('Yatsıya');
                    calcDate.setHours(value.eveningPrayer.hours);
                    calcDate.setMinutes(value.eveningPrayer.minutes);
                } else {
                    $('.lblPrayerLabel').html('İmsak');
                    calcDate.setHours(value.dawnTime.hours);
                    calcDate.setMinutes(value.dawnTime.minutes);
                    if (currentHour <= 24) {
                        calcDate.setDate(calcDate.getDate()+1);
                    }
                }

                var arrHourMinute = diffTime(calcDate);
                var hours = arrHourMinute[0];
                var minutes = arrHourMinute[1];

                interval = setInterval(function () {

                    arrHourMinute = diffTime(calcDate);
                    hours = arrHourMinute[0];
                    minutes = arrHourMinute[1];

                    if (hours < 0) {
                        clearInterval(interval);
                        $(".lblPrayerTime").html("-");
                        prayerTimeCountDown(_idCity);
                    }
                }, 30000);
            }
        }
    });   
}

function diffTime(_calcDate) {
    var now = new Date().getTime();
    var distance = _calcDate.getTime() - now;
    var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
    //var seconds = Math.floor((distance % (1000 * 60)) / 1000);

    if (hours < 0 || minutes < 0 || (hours == 0 && minutes==0)) {
        $(".lblPrayerTime").html("0dk");
    }else if (hours != 0) {
        if (minutes != 0) {
            $(".lblPrayerTime").html(hours + "s " + minutes + "dk");
        } else {
            $(".lblPrayerTime").html(hours + "s");
        }
    } else {
        $(".lblPrayerTime").html(minutes + "dk");
    }

    return [hours, minutes];
}

//Prayer City Url
function getPrayerCityUrl(_idCity) {
    var citiesEnum = {
        adana: 1,
        adiyaman: 2,
        afyon: 3,
        agri: 4,
        amasya: 5,
        ankara: 6,
        antalya: 7,
        artvin: 8,
        aydin: 9,
        balikesir: 10,
        bilecik: 11,
        bingol: 12,
        bitlis: 13,
        bolu: 14,
        burdur: 15,
        bursa: 16,
        canakkale: 17,
        cankiri: 18,
        corum: 19,
        denizli: 20,
        diyarbakir: 21,
        edirne: 22,
        elazig: 23,
        erzincan: 24,
        erzurum: 25,
        eskisehir: 26,
        gaziantep: 27,
        giresun: 28,
        gumushane: 29,
        hakkari: 30,
        hatay: 31,
        isparta: 32,
        mersin: 33,
        istanbul: 34,
        izmir: 35,
        kars: 36,
        kastamonu: 37,
        kayseri: 38,
        kirklareli: 39,
        kirsehir: 40,
        kocaeli: 41,
        konya: 42,
        kutahya: 43,
        malatya: 44,
        manisa: 45,
        kahramanmaras: 46,
        mardin: 47,
        mugla: 48,
        mus: 49,
        nevsehir: 50,
        nigde: 51,
        ordu: 52,
        rize: 53,
        sakarya: 54,
        samsun: 55,
        siirt: 56,
        sinop: 57,
        sivas: 58,
        tekirdag: 59,
        tokat: 60,
        trabzon: 61,
        tunceli: 62,
        sanliurfa: 63,
        usak: 64,
        van: 65,
        yozgat: 66,
        zonguldak: 67,
        aksaray: 68,
        bayburt: 69,
        karaman: 70,
        kirikkale: 71,
        batman: 72,
        sirnak: 73,
        bartin: 74,
        ardahan: 75,
        igdir: 76,
        yalova: 77,
        karabuk: 78,
        kilis: 79,
        osmaniye: 80,
        duzce: 81
    };
    return getBaseUrl()+Object.keys(citiesEnum).find(key => citiesEnum[key] === _idCity)+"-namaz-vakitleri";
}