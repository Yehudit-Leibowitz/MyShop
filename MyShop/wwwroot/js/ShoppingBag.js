
const Basket = addEventListener("load", async () => {
    DrawBacket()
    //let categoryIdArr = [];
    //let basketarr = [];
    //sessionStorage.setItem("categoryIds", JSON.stringify(categoryIdArr))
    //sessionStorage.setItem("basket", JSON.stringify(basketarr))
})


const DrawBacket = async () => {
  
    let products = JSON.parse(sessionStorage.getItem("basket"))

    for (let i = 0; i < products.length; i++) 
        await showProductBasket(products[i])
   
}

const showProductBasket = async (product) => {
    const inbasket = await fetch(`api/products/${product}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        },

        query: {
            id: product
        }


    });
    shopingBag = await inbasket.json();
   console.log(shopingBag)
    showOneProduct(shopingBag);
 
}
const showOneProduct = async (product) => {
    const url =`./Pictures/${product.image}`
    let tmp = document.getElementById("temp-row");
    let cloneProduct = tmp.content.cloneNode(true)
    cloneProduct.querySelector(".image").style.backgroundImage  = `url(${url})` 
    cloneProduct.querySelector(".availabilityColumn").innerText = true;
    cloneProduct.querySelector(".itemName").textContent = product.productName
    cloneProduct.querySelector(".price").innerText = product.price
    cloneProduct.getElementById("delete").addEventListener('click', () => {
        deleteItem(product)
    })
    document.querySelector("tbody").appendChild(cloneProduct)
};

const deleteItem = (product) => {
    products = JSON.parse(sessionStorage.getItem("basket"))
    let j = 0
    for (j = 0; j < products.length; j++) {

        if (products[j] == product.productId) {
            break;
        }
    }
    products.splice(j, 1)
    sessionStorage.setItem("basket", JSON.stringify(products))
    window.location.href = "ShoppingBag.html"
    getOrderProducts()
}
const detials = () => {
    let UserId = JSON.parse(sessionStorage.getItem("user")).userId
    console.log(UserId)
    let orderItems1 = JSON.parse(sessionStorage.getItem("basket"))
    const OrderItems = []
    orderItems1.map(t => {
        let object = { productId:t ,qantity: 1 }

        OrderItems.push(object)
    })

    let OrderSum = 100
    let OrderDate =  new Date();
   
    console.log(OrderDate)
    return ({
        OrderDate,OrderSum, UserId, OrderItems, 
    })
}

//const placeOrder = async () => {
//    let alldetials = detials()
//    const orderss = await fetch(`https://localhost:44379/api/Orders`, {
//        method: 'POST',
//        headers: {
//            'Content-Type': 'application/json'
//        },
//        body: alldetials
//    });
//    alldetialss = await orderss.json();
//    if (orderss.ok) {
//        alert("nice")
//        sessionStorage.setItem("basket", JSON.stringify([]))
//        location.reload()
//        window.location.href = "Products.html";

//    }

//    else
//        alert("Something went wong , try again")
//}

    


placeOrder = async () => {
    //let user = JSON.parse(sessionStorage.getItem("id")) || null;
    //if (user == null) {
    //    window.location.href = "Login.html"
    //}
    //let shoppingBag = JSON.parse(sessionStorage.getItem("orderItems")) || [];
    //let products = []
    //let sum = 0
    //for (let i = 0; i < shoppingBag.length; i++) {
    //    let thisProduct = { ProductId: shoppingBag[i].productId, Quentity: 1 }
    //    console.log(shoppingBag[i].productId)
    //    sum += shoppingBag[i].price
    //    products.push(thisProduct)
    //}
    let alldetials = detials()
    try {

        const orderPost = await fetch("https://localhost:44379/api/Orders", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(alldetials)

        });
        if (orderPost.status == 204)
            alert("Not found product")
        if (!orderPost.ok)
            throw new Error(`HTTP error! status:${orderPost.status}`)
        const data = await orderPost.json()
        console.log(data)
        alert(`order number ${data.orderId} seccied!`)
        sessionStorage.setItem("orderItems", JSON.stringify([]))
        window.location.href = "Products.html"

    }
    catch (error) {
        alert("try again" + error)
        console.log(error)
    }
}


