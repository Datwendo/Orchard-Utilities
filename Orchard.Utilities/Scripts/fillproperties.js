/* CS fill properties  */
jQuery(function ($) {

    $(".partname").on("change", function (event) {
    event.preventDefault();
    var srvurl = $("#ajaxurl").val();
    var pattern = /\_(\d+)\__/;
    var parentId = $(this).attr('id').match(pattern)[1];
    $.ajax({
        url: srvurl,
        type: 'POST',
        dataType: "",
        data: encodedata($(this).val()),
        error: function (xhr, ajaxOptions, thrownError) {
            return false;
        },
        success: function (data, textStatus, jqXHR) {
            var items = [];
            $(data.Properties).each(function (index, item) {
                items.push('<option>' + item + '</option>');
            });
            $('.propertyname').each(function (index, itm) {
                if ($(this).attr('id').search('_'+parentId+'__') != -1)
                {
                    $(this).empty().append.apply($(this), items);
                }
            });

            var flds = [];
            $(data.Fields).each(function (index, item) {
                flds.push('<option value="'+item.Name +'">' + item.DisplayName + '</option>');
            });
            $('.fieldname').each(function (index, itm) {
                if ($(this).attr('id').search('_' + parentId + '__') != -1) {
                    $(this).empty().append.apply($(this), flds);
                }
            });
        }
    });
    });

    encodedata = function (partname) {
        var x = {
            partName: partname,
            __RequestVerificationToken: antiForgeryToken
        };
        return x;
    };
});