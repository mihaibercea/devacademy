//prefill with valid values
document.getElementById("title").value = "Clean Code";
document.getElementById("author").value = "Robert C. Martin";
document.getElementById("ISBN10").value = "9780132350884";
document.getElementById("ISBN13").value = "978-0132350884";
document.getElementById("publishedDate").value = "01/08/2008";
document.getElementById("numberOfPages").value = 464;
document.getElementById("publisher").value = "Pearson";
document.getElementById("reviewScore").value = 4.38;

console.log("Insert book page loaded");

function clickedOnSubmit() {
    let valid = true;
    console.log("Clicked on submit");

    let mandatoryFields = ["title", "author", "ISBN10", "ISBN13", "publishedDate", "numberOfPages", "publisher", "reviewScore"];

    mandatoryFields.forEach((field) => {
        document.getElementById(`${field}Error`).hidden = true;
        if (document.getElementById(field).value == "") {
            document.getElementById(`${field}Error`).hidden = false;
            document.getElementById(`${field}Error`).innerHTML = `Please fill in the ${field} field<br/>`;
            valid = false;
        }
    });


    if (!document.getElementById("publishedDate").value == "") {
        //check the format, ex: 21/04/2024
        let inputDate = document.getElementById("publishedDate").value;

        valid = validateDate(inputDate) && valid;
    }
    if (!document.getElementById("numberOfPages").value == "") {
        let numberofPages = document.getElementById("numberOfPages").value;
        valid = validateNumberOfPages(numberofPages) && valid;

    }

    if (!valid) {
        return;
    }

    let bodyContent = {
        "title": document.getElementById("title").value,
        "author": document.getElementById("author").value,
        "ISBN10": document.getElementById("ISBN10").value,
        "ISBN13": document.getElementById("ISBN13").value,
        "publishedDate": document.getElementById("publishedDate").value,
        "numberOfPages": document.getElementById("numberOfPages").value,
        "publisher": document.getElementById("publisher").value,
        "reviewScore": document.getElementById("reviewScore").value
    }

    fetch('http://localhost:62509/api/book',
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(bodyContent)
        })
        .then((response) => {
            console.log('Response: ', response);
            return response.json();
        })
        .then((data) => {
            console.log('Data: ', data);
            alert("Book inserted successfully");
        })
        .catch((error) => {
            console.error('Error: ', error);
            alert("Error inserting book");
        });
}


function validateDate(inputDate) {
    let split = inputDate.split("/");
    if (split.length != 3) {
        document.getElementById("publishedDateError").hidden = false;
        document.getElementById("publishedDateError").innerHTML = "Please fill in the Published Date in the dd//MM/yyyy format<br/>";
        return false;
    } else {
        let day = split[0];
        let month = split[1];
        let year = split[2];
        if (day < 1 || day > 31 || month < 1 || month > 12 || year < 1900 || year > 2030) {
            document.getElementById("publishedDateError").hidden = false;
            document.getElementById("publishedDateError").innerHTML = "Please fill in the Published Date in the dd//MM/yyyy format<br/>";
            return false;
        }
    }
    return true;
}

function validateNumberOfPages(numberofPages) {
    if (isNaN(numberofPages)) {
        document.getElementById("numberOfPagesError").hidden = false;
        document.getElementById("numberOfPagesError").innerHTML = "Please fill in the Number Of Pages field with a number<br/>";
        return false;
    } else {
        if (numberofPages < 1) {
            document.getElementById("numberOfPagesError").hidden = false;
            document.getElementById("numberOfPagesError").innerHTML = "Please fill in the Number Of Pages field with a number greater than 0<br/>";
            return false;
        }
    }
    return true;
}