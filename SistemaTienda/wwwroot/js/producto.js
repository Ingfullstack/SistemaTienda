let datatable;

$(document).ready(() => {
    CargarTabla();
});

const CargarTabla = () => {
    datatable = $("#tblDatos").DataTable({
        "ajax": {
            "url": "/Admin/Productos/ObtenerTodos"
        },
        "columns": [
            { data: "codigo"},
            { data: "descripcion"},
            { data: "categoria.nombre"},
            { data: "marca.nombre"},
            {
                data: "urlImagen",
                "render": function (data) {
                    return `<img src="../${data}" class="imagen"/>`;
                }
            },
            {
                data: "precio", className: "text-end",
                "render": function (dinero) {
                    var f = "$";
                    var d = f + dinero.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
                    return d;
                }
            },
            {
                data: "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Productos/Editar/${data}" class="btn btn-success btn-sm bg-gradient"> <i class="bi bi-pencil-square"></i> </a>
                                <a onclick=Eliminar("/Admin/Productos/Eliminar/${data}") class="btn btn-danger btn-sm bg-gradient"> <i class="bi bi-trash"></i> </a>
                            </div>`;
                }
            },
        ]
    });
};

const Eliminar = (url) => {
    Swal.fire({
        title: "Estas Seguro?",
        text: "No podras revertir esto!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si Eliminar!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}

