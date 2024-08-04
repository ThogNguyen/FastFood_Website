/*sweetalert (cancel-order-button)*/
document.addEventListener('DOMContentLoaded', function () {
    const cancelButtons = document.querySelectorAll('.btn-cancel-order');

    cancelButtons.forEach(button => {
        button.addEventListener('click', function () {
            swal({
                title: "Bạn có chắc muốn hủy đơn không?",
                icon: "warning",
                buttons: {
                    cancel: {
                        text: "Không",
                        value: null,
                        visible: true,
                        className: "",
                        closeModal: true,
                    },
                    confirm: {
                        text: "Đồng ý",
                        value: true,
                        visible: true,
                        className: "btn-danger",
                        closeModal: true
                    }
                },
                dangerMode: true,
            })
                .then((willDelete) => {
                    if (willDelete) {
                        swal("Hủy đơn thành công!", {
                            icon: "success",
                            button: "Đóng"
                        });
                    } else {
                        swal("Đã hủy thao tác!", {
                            button: "Đóng"
                        });
                    }
                });
        });
    });
});


