const productList = addEventListener("load", async () => {
    let categoriesIdArr = [];
    let basketArr = JSON.parse(sessionStorage.getItem("basket")) || [];
    sessionStorage.setItem("categoriesId", JSON.stringify(categoriesIdArr))
    sessionStorage.setItem("basket", JSON.stringify(basketArr))
    basketArr = JSON.parse(sessionStorage.getItem("basket")) || [];
    document.querySelector("#ItemsCountText").innerHTML = basketArr.length
    drawProducts();
    showAllCategories();
})

const getDetailsFromForm = async () => {
    document.getElementById("PoductList").innerHTML = ''
    let search = {
        nameSearch: document.querySelector("#nameSearch").value,
        minPrice: parseInt(document.querySelector("#minPrice").value),
        maxPrice: parseInt(document.querySelector("#maxPrice").value)
    }
    return search
}
const filterProducts = async () => {//why do you need the filter func? call draw.
    drawProducts()
}
const drawProducts = async () => {
    const categoriesIdFromSession = JSON.parse(sessionStorage.getItem("categoriesId"))
    console.log(categoriesIdFromSession)
    let { nameSearch, minPrice, maxPrice } = await getDetailsFromForm()
    let url = `https://localhost:44379/api/products`
    if (nameSearch || minPrice || maxPrice || categoriesIdFromSession) {
        url += '?'
        if (nameSearch != '')
            url += `&desc=${nameSearch}`
        if (minPrice)
            url += `&minPrice=${minPrice}`
        if (maxPrice)
            url += `&maxPrice=${maxPrice}`
        if (categoriesIdFromSession != []) {
            for (let i = 0; i < categoriesIdFromSession.length; i++) {//map or foreach is nicer
                url += `&categoryIds=${categoriesIdFromSession[i]}`
            }
        }
    }
    try {
        const allProducts = await fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            query: {
                desc: nameSearch,
                minPrice: minPrice,
                maxPrice: maxPrice,
                categoriesId: categoriesIdFromSession
            }
        });
        const dataProducts = await allProducts.json();

        console.log('GET Data:', dataProducts)
        showAllProducts(dataProducts);
    }
    catch (error) {
        alert("Something wrong, try again")
    }
}
const showAllProducts = async (products) => {
    for (let i = 0; i < products.length; i++) {//foreach is nicer
        showOneProduct(products[i]);
    }
}

const showOneProduct = async (product) => {
    let temp = document.getElementById("temp-card");
    let cloneProduct = temp.content.cloneNode(true)
    if (product.image)
    cloneProduct.querySelector("img").src = "../pictures/" + product.image
    cloneProduct.querySelector("h1").textContent = product.productName
    cloneProduct.querySelector(".price").innerText = product.price + '$'
    cloneProduct.querySelector(".description").innerText = product.description
    cloneProduct.querySelector("button").addEventListener('click', () => { addToCart(product) })
    document.getElementById("PoductList").appendChild(cloneProduct)
}
const addToCart = (product) => {

    if (sessionStorage.getItem("user")) {

        let productsInbasket = JSON.parse(sessionStorage.getItem("basket"))
        productsInbasket.push(product.productId)
        sessionStorage.setItem("basket", JSON.stringify(productsInbasket))
        document.querySelector("#ItemsCountText").innerHTML = productsInbasket.length
        alert("Successfully added to cart")
    }
    else {
        alert("Unregistered user")
        setTimeout(() => {
            window.location.href = "../LogIn.html"
        }, "3000");
      
    }

}


const showAllCategories = async () => {//divide the get to a different func.

    const ReceivedCategories = await fetch('https://localhost:44379/api/Categories', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        },

    });
    allCategories = await ReceivedCategories.json();
    for (let i = 0; i < allCategories.length; i++) {//forEach
        showOneCategory(allCategories[i]);

    }
}
const showOneCategory = async (category) => {

    let temp = document.getElementById("temp-category");
    let cloneProduct = temp.content.cloneNode(true)

    cloneProduct.querySelector(".OptionName").textContent = category.categoryName
    cloneProduct.querySelector(".opt").addEventListener('change', () => { filterCategory(category) })
    document.getElementById("categoryList").appendChild(cloneProduct)
}

const filterCategory = (category) => {
    let categories = JSON.parse(sessionStorage.getItem("categoriesId"))
    let a = categories.indexOf(category.categoryId
)
    a == -1 ? categories.push(category.categoryId) : categories.splice(a, 1)
    sessionStorage.setItem("categoriesId", JSON.stringify(categories))
    drawProducts()
}

