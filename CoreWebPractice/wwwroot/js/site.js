// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*modal*/
$(function () {
    // boostrap 4 load modal example from docs
    $('.modal-container').on('show.bs.modal', function (event) {

        var button = $(event.relatedTarget); // Button that triggered the modal

        var url = button.attr("href");

        var modal = $(this);

        // note that this will replace the content of modal-content everytime the modal is opened
        modal.find('.modal-content').load(url);

    });

    $('.modal-container').on('hidden.bs.modal', function () {

        // remove the bs.modal data attribute from it

        $(this).removeData('bs.modal');

        // and empty the modal-content element

        //$('.modal-container .modal-content').empty();

        $(this).find('.modal-content').empty();

    });


    $(".toggle-menu-size-btn").click(function () {
        $("body").toggleClass("mini-side-menu");
    })
});

$(".had-sub-menu").click(function () {
    $(this).toggleClass("active");
});

//Multiple modals overlay z-index fix
$(document).on('show.bs.modal', '.modal', function () {
    var zIndex = 1040 + (10 * $('.modal:visible').length);
    $(this).css('z-index', zIndex);
    setTimeout(function () {
        $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
    }, 0);
});
/*modal end*/


/* Autocomplete  */
$(function () {
    autocompleteInit();
});
function autocompleteInit() {
    $('.autocomplete').each(function (index) {
        var arr = [];    
        $(this).find('.autocomplete-datasource').find('.autocomplete-datasource-item').each(function (idx, ele) {
            arr.push($(ele).attr('autocomplete-datasource-item'));

        });
        //console.log(arr);       
        autocomplete($(this).find('input')[0], arr);       
    });
    $(this).find('.autocomplete-datasource').remove();
    /*execute a function when someone clicks in the document:*/
    document.addEventListener("click", function (e) {       
        if (e.target.parentNode.classList.contains('autocomplete') || e.target.parentNode.classList.contains('autocomplete-items')) 
            return;       
        var x = document.getElementsByClassName("autocomplete-items");
        for (var i = 0; i < x.length; i++) {
            x[i].parentNode.removeChild(x[i]);
        }
    });
};
function autocomplete(inp, arr) {
    arr = arr || [];
    /*the autocomplete function takes two arguments,
    the text field element and an array of possible autocompleted values:*/
    var currentFocus;
    /*execute a function when someone writes in the text field:*/
    inp.addEventListener("input", function (e) {
        /*close any already open lists of autocompleted values*/
        closeAllLists();
        if (!this.value) { return false; }
        createList(inp, this);
    });
    /*execute a function presses a key on the keyboard:*/
    inp.addEventListener("keydown", function (e) {
        var x = document.getElementById(this.id + "autocomplete-list");
        if (x) x = x.getElementsByTagName("div");
        if (e.keyCode == 40) {
            /*If the arrow DOWN key is pressed,
            increase the currentFocus variable:*/
            currentFocus++;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 38) { //up
            /*If the arrow UP key is pressed,
            decrease the currentFocus variable:*/
            currentFocus--;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 13) {
            /*If the ENTER key is pressed, prevent the form from being submitted,*/
            e.preventDefault();
            if (currentFocus > -1) {
                /*and simulate a click on the "active" item:*/
                if (x) x[currentFocus].click();
            }
        }
    });
    inp.addEventListener("click", function (e) {

        /*close any already open lists of autocompleted values*/
        closeAllLists();
        createList(inp, this);

    });
    function createList(inp, doc) {

        var a, b, i, val = doc.value;
        currentFocus = -1;
        /*create a DIV element that will contain the items (values):*/
        a = document.createElement("DIV");
        a.setAttribute("id", doc.id + "autocomplete-list");
        a.setAttribute("class", "autocomplete-items");
        
        /*append the DIV element as a child of the autocomplete container:*/
        doc.parentNode.appendChild(a);
        /*for each item in the array...*/
        for (i = 0; i < arr.length; i++) {
            /*check if the item starts with the same letters as the text field value:*/
            if (arr[i].substr(0, val.length).toUpperCase() == val.toUpperCase()) {
                /*create a DIV element for each matching element:*/
                b = document.createElement("DIV");
                /*make the matching letters bold:*/
                b.innerHTML = "<strong>" + arr[i].substr(0, val.length) + "</strong>";
                b.innerHTML += arr[i].substr(val.length);
                /*insert a input field that will hold the current array item's value:*/
                b.innerHTML += "<input type='hidden' value='" + arr[i] + "'>";
                /*execute a function when someone clicks on the item value (DIV element):*/
                b.addEventListener("click", function (e) {
                    /*insert the value for the autocomplete text field:*/
                 
                    inp.value = this.getElementsByTagName("input")[0].value;
                    /*close the list of autocompleted values,
                    (or any other open lists of autocompleted values:*/
                    closeAllLists();
                });
                a.appendChild(b);
            }
        }
    }
    function addActive(x) {
        /*a function to classify an item as "active":*/
        if (!x) return false;
        /*start by removing the "active" class on all items:*/
        removeActive(x);
        if (currentFocus >= x.length) currentFocus = 0;
        if (currentFocus < 0) currentFocus = (x.length - 1);
        /*add class "autocomplete-active":*/
        x[currentFocus].classList.add("autocomplete-active");
    }
    function removeActive(x) {
        /*a function to remove the "active" class from all autocomplete items:*/
        for (var i = 0; i < x.length; i++) {
            x[i].classList.remove("autocomplete-active");
        }
    }
    function closeAllLists(elmnt) {
        /*close all autocomplete lists in the document,
        except the one passed as an argument:*/
        var x = document.getElementsByClassName("autocomplete-items");
        for (var i = 0; i < x.length; i++) {
            if (elmnt != x[i] && elmnt != inp) {
                x[i].parentNode.removeChild(x[i]);
            }
        }
    }
  
}

/* Autocomplete end */
/**
 * check if string contains html tag
 * @param {any} str
 */
function isHTML(str) {
    var a = document.createElement('div');
    a.innerHTML = str;

    for (var c = a.childNodes, i = c.length; i--;) {
        if (c[i].nodeType == 1) return true;
    }

    return false;
}

