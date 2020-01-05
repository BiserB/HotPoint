
$(document).ready(function () {

    const cartName = "hotPointCart";

    $.ajax({
        url: "https://localhost:44348/api/shopping/getCart",
        dataType: 'json',
        type: 'get',
        contentType: 'application/json; charset=utf-8',
        success: function (cart) {

            console.log("cart..", cart);

            cart.items.forEach(i => {
                let selector = "#" + i.productId;
                $(selector).data("value", i.quantity);
                $(selector).text(i.quantity);
            });

            $(".add-big-box").on("click", function () {
                
                var item = {};
                item.productId = parseInt($(this).data("id"));

                var productCartCount = $("#" + item.productId);

                $.ajax({
                    url: "https://localhost:44348/api/shopping/addToCart",
                    dataType: 'json',
                    type: 'post',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(item),
                    success: function (updatedCart) {
                        console.log("updatedCart..", updatedCart);
                        cart = updatedCart;

                        var currentItem = cart.items.find(i => i.productId === item.productId);

                        productCartCount.data("value", currentItem.quantity);
                        productCartCount.text(currentItem.quantity);
                        
                        $(".badge-value").data("value", cart.items.length);
                        $(".badge-value").text(cart.items.length);
                    },
                    error: function () {
                        console.log("Error");
                    }
                });
                
            });

            $(".remove-big-box").on("click", function () {

                var item = {};
                item.productId = parseInt($(this).data("id"));

                var productCartCount = $("#" + item.productId);

                $.ajax({
                    url: "https://localhost:44348/api/shopping/removeFromCart",
                    dataType: 'json',
                    type: 'post',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(item),
                    success: function (updatedCart) {
                        console.log("updatedCart..", updatedCart);
                        cart = updatedCart;

                        var currentItem = cart.items.find(i => i.productId === item.productId);

                        if (currentItem) {
                            productCartCount.data("value", currentItem.quantity);
                            productCartCount.text(currentItem.quantity);
                        } else {
                            productCartCount.data("value", 0);
                            productCartCount.text("");
                        }                 

                        $(".badge-value").data("value", cart.items.length);
                        $(".badge-value").text(cart.items.length);
                    },
                    error: function () {
                        console.log("Error");
                    }
                });

            });

            $(".shopping-cart").on("click", function () {

                var cartContent = $("#cart-content");
                cartContent.empty();

                cart.items.forEach(i => {

                    cartContent.append("<p><strong> " + i.productName + "</strong> - " + i.quantity + " бр.</p>");
                });

            });

        },
        error: function () {
            console.log("Error");
        }
    });

});

