var wordsArray = [];
var mybag = 0;
var sum = 0;
var summa = 18;
var count = 0;
function addTodo(value, malue, balue,coun) {
    // Retrieve existing data from localStorage
    var existingData = localStorage.getItem('productObjects');
    // Parse existing data into an array or initialize an empty array
    var productObjects = existingData ? JSON.parse(existingData) : [];
    // Create a new object with the provided data
    var newProductObject = {
        ad: value,
        qiymet: malue,
        sekil: balue,
        say:coun
    };
    // Push the new object into the array
    productObjects.push(newProductObject);
    // Convert the updated array back to a JSON string
    var updatedData = JSON.stringify(productObjects);
    // Store the updated JSON string back into localStorage
    localStorage.setItem('productObjects', updatedData);
}

// Function to delete item from localStorage by index
function deleteItemByIndex(index) {
    // Get the localStorage data as an array
    var productObjects = JSON.parse(localStorage.getItem('productObjects')) || [];

    // Check if the index is valid
    if (index >= 0 && index < productObjects.length) {
        // Remove the item from the array
        productObjects.splice(index, 1);

        // Store the updated array back into localStorage
        localStorage.setItem('productObjects', JSON.stringify(productObjects));
        console.log('Item at index', index, 'removed from localStorage.');
    } else {
        console.log('Invalid index or no item at index:', index);
    }
}

// Example usage: delete item at index 2



function removeItemByAd(adValue) {
    // Get the keys from localStorage
    var keys = Object.keys(localStorage);

    // Iterate through the keys
    keys.forEach(function (key) {
        // Check if the key belongs to "productObjects"
        if (key === "productObjects") {
            // Get the value associated with the key
            var items = JSON.parse(localStorage.getItem(key));

            // Iterate through the items
            items.forEach(function (item, index) {
                // Log the "ad" property value
                console.log('Item:', item);

                // Check if the "ad" property of the item matches the provided value
                if (item && item.ad === adValue) {
                    // Remove the item from the array
                    items.splice(index, 1);
                    // Update the localStorage with the modified array
                    localStorage.setItem(key, JSON.stringify(items));
                    console.log('Item with ad:', adValue, 'removed from localStorage.');
                    return; // Exit the loop once an item is removed
                }
            });
        }
    });
}

// Example usage: remove an item with the "ad" property value of "Sandal For Women $888.00"
//removeItemByAd("Sandal For Women $888.00");

function getH4Text(iconElement) {
    var trElement = iconElement.closest('tr');
    var h4TdElement = trElement.querySelector('.storage');
    var h4Text = h4TdElement.querySelector('h4').textContent;
    deleteItemByIndex(h4Text);
    alert(h4Text); 
}

function sesd(){
    alert("dillaguska");
};

function addto_cart(element) {
    mybagg()
    if (element.closest('.product__name')) {
        var productThumb = element.closest('.product__thumb');
        var image = productThumb.querySelector('img.product-primary');
        var imageName = image.getAttribute('src');
        
        var nearbyH4 = element.closest('.product__name').querySelector('h4');
        var nearbySpan = element.closest('.product__name').querySelector('span');

        if (nearbyH4 || nearbySpan) {
            var nearbyH4Text = nearbyH4.textContent.trim(); // Trim any leading or trailing whitespace
            var nearbySpanText = nearbySpan.textContent.trim();
            var productName = nearbyH4Text + ' ' + nearbySpanText;
            let dollar = nearbySpanText.slice(1); 
            //localStorage.clear();
           // imageName = productName.replace(/\s+/g, '_').toLowerCase() + '.jpg';
            // alert(nearbyH4Text + dollar + imageName);
          //  alert('zurnaqulam');
            count++;
            addTodo(productName, dollar, imageName, count);
            location.reload();
        } else {
            console.log("No <h4> or <span> element found within the clicked div."); 

        }
    }
   
}

function mikol() {
    var yekunElements = document.querySelectorAll('.yekun');
    var total = document.querySelector('#totalik');


    yekunElements.forEach(function (element) {
        // Parse the text content as a float and add it to the sum
        summa += parseFloat(element.textContent.replace(',', '.'));
    });
    total.textContent = summa;
   // alert(sum);
}

function mybagg() {
    var total = document.querySelector('.counter');
    var alltab = document.querySelectorAll('.add_cart_product');
    var sum = 0;
    alltab.forEach(function (element) {
       
        sum += 1;
    });
    total.textContent = ' ' + sum;
 
}

function getText(iconElement) {
    var trElement = iconElement.closest('tr');
    var h4TdElement = trElement.querySelector('.yekun');
    var hspan = h4TdElement.querySelector('span').textContent;
    alert(hspan);
}

function incrementValue(iconElement) {
    // Get the input element by its ID
    var numberInput = document.getElementById('myNumberInput');

    // Get the value of the input element
    var valued = parseFloat(numberInput.value);

    // Get the total element
    var total = document.querySelector('#totalik');

    // Find the closest <tr> element to the icon
    var trElement = iconElement.closest('tr');

    // Find the <td> element with class 'yekun'
    var yekunTdElement = trElement.querySelector('.yekun');

    // Find the <span> element within the <td>
    var spanElement = yekunTdElement.querySelector('span');

    // Get the current value from the <span> element
    var currentValue = parseFloat(spanElement.textContent.trim());

    // Calculate the new value by multiplying the current value with the value of the input
    var newValue = currentValue * valued;

    // Update the <span> element with the new value
    spanElement.textContent = newValue;

    // Update the total sum
    var totalValue = parseFloat(total.textContent.trim()) || 0;
    total.textContent = totalValue + newValue;
};

mybagg();


