


for (let i = 1; i <= 5; i++)
{
    document.getElementById("cat_"+i).addEventListener("mouseover", ShowCRUD)
    document.getElementById("cat_"+i).addEventListener("mouseout", HideCRUD)



    document.getElementById("crud_"+i).style.visibility="hidden"


    function ShowCRUD() {
        document.getElementById("crud_" + i).style.visibility = "visible"
    }

    function HideCRUD() {
        document.getElementById("crud_" + i).style.visibility = "hidden"
    }
}

