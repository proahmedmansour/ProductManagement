$(function () {
    /*ABP allows you to reuse the localization texts in your JavaScript code.In this way, you
    can define them on the server side and use them on both sides.abp.localization.
    getResource returns a function to localize the values.*/
    var l = abp.localization.getResource('ProductManagement');

    //abp.libs.datatables.normalizeConfiguration is a helper function defined by ABP Framework.
    //It simplifies the data table's configuration by providing conventional default values for missing options.
    var dataTable = $('#ProductsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[0, "asc"]],
            searching: false,
            scrollX: true,
            /* helper function that adapts ABP's dynamic JavaScript client proxies to the data table's parameter format.*/
            ajax: abp.libs.datatables.createAjax(
                productManagement.products.product.getList),
            columnDefs: [
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('CategoryName'),
                    data: "categoryName",
                    orderable: false
                },
                {
                    title: l('Price'),
                    data: "price"
                },
                {
                    title: l('StockState'),
                    data: "stockState",
                    render: function (data) {
                        return l('Enum:StockState:' + data);
                    }
                },
                {
                    title: l('CreationTime'),
                    data: "creationTime",
                    dataFormat: 'date'
                }
            ]
        })
    );

    var createModal = new abp.ModalManager(abp.appPath + 'Products/CreateProductModal');
    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewProductButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
