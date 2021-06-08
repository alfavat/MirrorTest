$(function ($) {
    "use strict";

    var swiper_top_main = new Swiper('.swiper-container-main', {
        loop: true,
        navigation: {
            nextEl: '.swiper-button-next-main',
            prevEl: '.swiper-button-prev-main',
        },
        pagination: {
            el: '.swiper-pagination-main',
            clickable: false,
        },
        autoplay: {
            delay: 7000,
            disableOnInteraction: false,
        },
    });

    $(document).ready(function () {

        //Breaking News
        $('#newsTicker').breakingNews({
            effect: 'slide-down'
        });

    });
});

function pad(num, size) {
    var s = num + "";
    while (s.length < size) s = "0" + s;
    return s;
}


function getDate(dt) {// 2018-05-01 > 01.05.2018
    var d = new Date(dt);
    return pad(d.getDate(), 2) + "." + pad((d.getMonth() + 1), 2) + '.' + d.getFullYear();
}

function getDateTime(dt) {
    var d = new Date(dt);
    return pad(d.getDate(), 2) + "." + pad((d.getMonth() + 1), 2) + '.' + d.getFullYear() + ' ' + pad(d.getHours(), 2) + ':' + pad(d.getMinutes(), 2);
}