let deletebuttons = document.querySelectorAll(".delete-image-btn")

deletebuttons.forEach(btn => btn.addEventListener("click", function () {
    btn.parentElement.remove()
}))


let itemDeleteBtns = document.querySelectorAll(".item-delete");

itemDeleteBtns.forEach(btn => btn.addEventListener("click", function (e) {
    e.preventDefault();

    Swal.fire({
        title: 'Eminsiz?',
        text: "Geri qaytara bilmeyecesiniz!",
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Beli, silirem!',
        cancelButtonText: 'Yox qadan alim qalsin'
    }).then((result) => {
        if (result.isConfirmed) {
            let url = btn.getAttribute("href")
            fetch(url)
                .then(response => {
                    if (response.status == 200) {
                        // alert("silindi")
                        window.location.reload(true)
                    }
                    else {
                        alert("silmek istediyiniz tapilmayib")
                    }
                })
        }
    }) }))