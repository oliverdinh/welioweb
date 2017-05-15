
$("#input-file-avatar").click(function () {
    $(this).val("");
});
$('#div-select-avatar')
          .click(function () {
              $('#input-file-avatar').trigger('click');
          });
$('#changeImage')
    .click(function () {
        $('#input-file-avatar').trigger('click');
    });

var _URL = window.URL || window.webkitURL;
var w;
var h;
var size;
$('#input-file-avatar')
    .on('change',
        function () {
            if ($('#cropModal').css("display") === 'none') {
                $('#divOverlay').show();
            } else {
                $('#divOverlay2').show();
            }
            var file, img;
            if ((file = this.files[0])) {
                img = new Image();
                img.onload = function () {
                    w = this.width;
                    h = this.height;
                };
                img.src = _URL.createObjectURL(file);
            }
        });
$('.image-editor').cropit({
    exportZoom: 1,
    imageBackground: true,
    imageBackgroundBorderWidth: [10, 40, 10, 40],
    smallImage: 'allow',
    allowDragNDrop: true,
    width: 250,
    height: 250,
    onImageLoading: function () {
       

    },
    onImageLoaded: function () {
        $('#cropModal').modal('show');
        if (w < 250 || h < 250) {
            //$('.cropit-preview-background-container').css('display', 'none');
            $('.cropit-preview-image-container').css('border', '2px solid #eee');
        } else {
            $('.cropit-preview-image-container').css('border', '');
        }
        $('#divOverlay').hide();
        $('#divOverlay2').hide();
    },
    onImageError: function () {
        alert('Invalid image file.');
    }
});

$('#rotate-cw').click(function () {
    $('.image-editor').cropit('rotateCCW');
    return false;
});
$('#rotate-ccw').click(function () {
    $('.image-editor').cropit('rotateCW');
    return false;
});

$('.export').click(function () {
    var imageData = $('.image-editor').cropit('export');
    var name = $('#input-file-avatar').val().split('\\').pop();
    $('#imv-my-avatar').attr('src', imageData);
    $('#cropModal').modal('hide');
    $('.hidden-image-data').val(imageData);
    $('.hidden-image-name').val(name);
    return false;
});