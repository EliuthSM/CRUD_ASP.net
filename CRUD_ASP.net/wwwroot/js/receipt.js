$(document).ready(function () {
    // Obtener detalles desde el input hidden
    let detailsJson = $("#ReceiptDetailsJson").val();
    let details = detailsJson ? JSON.parse(detailsJson) : [];
    const $detailsBody = $("#detailsBody");
    const $btnAddDetail = $("#btnAddDetail");
    const $productNameInput = $("#ProductName");
    const $amountInput = $("#Amount");
    const $updatedReceiptDetailsJson = $("#UpdatedReceiptDetailsJson");

    function renderDetails() {
        $detailsBody.empty();
        $("#hiddenDetailsContainer").empty(); // Limpia los inputs ocultos

        details.forEach((item, index) => {
            const row = `<tr>
                        <td>${item.product_name}</td>
                        <td>${item.amount}</td>
                        <td>
                            <button data-index="${index}" class="btn btn-danger btn-sm btnDelete">Eliminar</button>
                        </td>
                    </tr>`;
            $detailsBody.append(row);

            // Agregar inputs ocultos para cada detalle
            $("#hiddenDetailsContainer").append(`
            <input type="hidden" name="ReceiptDetails[${index}].Product_name" value="${item.product_name}" />
            <input type="hidden" name="ReceiptDetails[${index}].Amount" value="${item.amount}" />
        `);
        });
    }


    $btnAddDetail.on("click", function () {
        const productName = $productNameInput.val().trim();
        const amount = parseFloat($amountInput.val());

        if (!productName || isNaN(amount) || amount <= 0) {
            alert("Ingrese un producto válido y un monto positivo.");
            return;
        }

        details.push({ product_name: productName, amount: amount });
        renderDetails();

        // Limpiar los inputs
        $productNameInput.val("");
        $amountInput.val("");
    });

    $detailsBody.on("click", ".btnDelete", function () {
        const index = $(this).data("index");
        details.splice(index, 1);
        renderDetails();
    });

    // Renderizar detalles al cargar la página
    renderDetails();
});
