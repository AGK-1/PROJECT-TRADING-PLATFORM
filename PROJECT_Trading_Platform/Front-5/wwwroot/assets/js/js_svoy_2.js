let addBasketBtns = document.querySelectorAll(".btn-primary")
let basketSale = document.getElementById("basketCountType")


let BasketArr;
if (localStorage.getItem("Basket") == null) {
    BasketArr = [];
} else {
    BasketArr = JSON.parse(localStorage.getItem("Basket"))
}

addBasketBtns.forEach(element => {
    element.addEventListener("click", function () {
        let id = this.parentElement.parentElement.getAttribute("data-id")
        let productName = this.parentElement.firstElementChild.innerText;
        let productImg = this.parentElement.previousElementSibling.getAttribute("src")
        let price = this.previousElementSibling.innerText;
        // console.log(id)
        // console.log(productName)
        // console.log(productImg)
        // console.log(price)

        let existProd = BasketArr.find(e => e.id == id);

        if (existProd == undefined) {
            let product = {
                id: id,
                name: productName,
                img: productImg,
                price: price,
                count: 1
            }
            BasketArr.push(product)
            console.log(BasketArr)
        } else {
            existProd.count += 1;
        }

        localStorage.setItem("Basket", JSON.stringify(BasketArr))
        addBasketDetail()
    })
});

function addBasketDetail() {
    basketSale.innerText = (JSON.parse(localStorage.getItem("Basket"))).length;
}

addBasketDetail()

