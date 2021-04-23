
function ProductDetail(params) {
    var _Params = params;
    var _ObjectReference = this;

    _Params.Type = "ProductDetails";
    _Params.InstanceCount = ClassLoader.InstanceCounter();
    console.log("INIT: " + _Params.Type + " - " + _Params.InstanceCount);


    this.Init = function () {
        debugger;
        $("#imageBrowes").change(function () {
            var File = this.files;
            if (File && File[0]) {
                ReadImage(File[0]);
            }
        })
    }


    var ReadImage = function (file) {
        debugger;

        var reader = new FileReader;
        var image = new Image;
        reader.readAsDataURL(file);
        reader.onload = function (_file) {

            image.src = _file.target.result;
            image.onload = function () {

                var height = this.height;
                var width = this.width;
                var type = file.type;
                var size = ~~(file.size / 1024) + "KB";

                $("#targetImg").attr('src', _file.target.result);
                $("#description").text("Size:" + size + ", " + height + "X " + width + ", " + type + "");
                $("#imgPreview").show();
            }
        }
    }

    var ClearPreview = function () {
        $("#imageBrowes").val('');
        $("#description").text('');
        $("#imgPreview").hide();
    }


    this.ShowInPopUp = function (url, title) {
        $.ajax({
            type: 'GET',
            url: url,
            data: {title:title},
            success: function (res) {
                $("#form-modal .modal-body").html(res);
                $("#form-modal .modal-header").html(title);
                $('#form-modal').modal('open');
                
            }, error: function (err) {
                $("#form-modal").modal('close');
            }
        })
    }

    this.closeModal = function () {
        $("#form-modal").modal('close');
    }

    //Post Add Form
    this.Add = function (form)
    {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                processData: false,
                contentType: false,
                dataType: "json",
                success: function (res) {
                    if (res.success == true) {
                        if (typeof (_Params.SaveProductCallBack) == "function") {
                            _Params.SaveProductCallBack({ ProductList: res.results});
                        }
                        $("#form-modal .modal-body").html('');
                        $("#form-modal .modal-header").html('');
                        $('#form-modal').modal('close');
                        $.notify(res.message, { globalposition: 'top center', className: 'success' });
                        location.reload();
                        //if add/Edit is success  route to Index page
                        document.location.href = APPLICATION_PATH + "/" + tenantName +"/Product/Product";
                    } else {
                        //$("#form-modal").modal('close');
                        $.notify(res.message, { globalposition: 'top center', className: 'error' });
                        //$.nofity is 3rd party notification libary like Toaster,Js is include in _Layout.cshtml
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
           return false;
    } 

    this.ProductApproval = function (form) {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            processData: false,
            contentType: false,
            dataType: "json",
            success: function (res) {
                if (res.success == true) {
                    debugger;
                    if (typeof (_Params.SaveProductCallBack) == "function") {
                        _Params.SaveProductCallBack({ ProductList: res.results });
                    }
                    $("#form-modal .modal-body").html('');
                    $("#form-modal .modal-header").html('');
                    $('#form-modal').modal('close');
                    $.notify(res.message, { globalposition: 'top center', className: 'success' });
                    //location.reload();
                    //if add/Edit is success  route to Index page
                    document.location.href = APPLICATION_PATH + "/" + tenantName + "/Product/Product";
                } else {
                    $("#form-modal").modal('close');
                    $.notify(res.message, { globalposition: 'top center', className: 'error' });
                    //$.nofity is 3rd party notification libary like Toaster,Js is include in _Layout.cshtml
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
        return false;
    }


    //Loaded Dependencies
    //Extends the base class    
    ClassLoader.LoadScript({
        Script:"/js/Base/BaseEntityView.js",
        ScriptLoadedCallback: function (params) {
            _ObjectReference.prototype = new BaseEntityView(_Params);
            _ObjectReference.RebindValidation = _ObjectReference.prototype.RebindValidation;
            _ObjectReference.RemoveEntity = _ObjectReference.prototype.RemoveEntity;
        }
    });
}