﻿$(document).ready(function () {
    //$page = $('.full-page');
    //image_src = $page.data('image');

    //if (image_src !== undefined) {
    //    image_container = '<div class="full-page-background" style="background-image: url(' + image_src + ') "/>';
    //    $page.append(image_container);
    //}
    
    $('.next').click(function ()
    {
        debugger
        $('.carousel').carousel('next');
        return false;
    });
    $('.prev').click(function ()
    {
        debugger
        $('.carousel').carousel('prev');
        return false;
    });
});