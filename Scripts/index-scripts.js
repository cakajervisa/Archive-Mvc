    /**
    * Funksioni i meposhtem hap dritaren per te ngarkuar nje dokument dhe i liston ato ne modal
    **/
    function NgarkoDokument(obj) {
    var filess = obj;
    var str;
    var msg = "";
    var tejkalim = false;
    document.getElementById("selectedFiles").innerHTML = "";
    for (var i = 0; i < filess.length; i++) {
        if (filess[i].size > 4194304) { //kontrollon nese madhesia tejkalon 4MB
            tejkalim = true;
            break;
        }
        else {
            str = "<div class='fusha-inline'>  " + filess[i].name + " <br></div>";
            document.getElementById("selectedFiles").innerHTML += str; //Liston dokumentat e ngarkuar
        }

    }
    if (tejkalim==true) {
        document.getElementById("selectedFiles").style.display = "none";
        document.getElementById("inpdokument").value = "";
        document.getElementById("skedari-file").innerHTML = "Dokumentat nuk duhet të jenë më shumë se 4 MB";
        setTimeout(() => {
             document.getElementById("skedari-file").innerHTML = "";
        }, 5000);


    }
    else {

        document.getElementById("selectedFiles").style.display = "block";
    }

}

    /**
    * Funksioni i meposhtem shfaq modalin per konfirmimin e fshirjes.
    **/
    function fshiDokument() {
        document.getElementById("alertModalKonfirmim").style.display = "block";
    }

    /**
    * Funksioni i meposhtem dergon kerkese tek controlleri per fshirjen e dokumentit.
    **/
    function KonfirmoFshirje() {
         var abc = document.getElementsByClassName("checkbox-1");
            var dokID;
            for (var ind = 0; ind < abc.length;ind++) {
                if (abc[ind].checked == true) {
                dokID = abc[ind].id.replace('checkbox-', '');
                    break;
                }
        }
              $.ajax({
                    url: '/Home/DeleteDok',
                    data: { 'id': dokID },
                    type: "post",
                    success: function (result) {
                        $('#divFolderat').html(result);},
                });
    }




    /**
    * Funksioni i meposhtem shfaq ose fsheh formen e filtrimit
    **/
    function FiltroDokumentat() {
        $("#divShto").hide();
       
        $("#divfilter").toggle();
        $("#titull").hide();
    }

    /**
    * Funksioni i meposhtem shfaq ose fsheh formen e shtimit te dokumentave
    **/
    function shtoDokumenta() {

    var spans = $('#tdpath').find("span");
    var spani = spans[4].getElementsByTagName("a")[0].getAttribute("href");
    var indf = spani.indexOf("=");
    var indm = spani.indexOf("&");
    var idd = spani.substring(indf+1, indm);
    $('#shtimInspektim').prop("value", idd);
        $("#titull").hide();
        $("#divfilter").hide();
        $("#divShto").toggle();
    }

    /**
    * Funksioni i meposhtem shfaq preview te dokumentit ne doubleclick
    **/
    function shfaqDokumentin(obj) {
    var dokID = obj;
    $.ajax({
        url: '/Home/GetTeDhenaDokumenti',
        data: { 'id': dokID },
        type: "post",
        success: function (result) {
            $('#ModalShikoDokument').html(result);
        },
    });

        var modal1 = document.getElementById("ModalShikoDokument");
    var blur1 = document.getElementsByClassName("jumbotron")[0];

    blur1.style.filter = "blur(30px)";
    blur1.style.filter = "opacity(30%)";
    modal1.style.display = "block";

    window.onclick = function (event) {

        if (event.target == modal1) {
            modal1.style.display = "none";
            blur1.style.filter = "blur(0px)";
            blur1.style.filter = "opacity(100%)";
        }
    }
}

    /**
    * Funksioni i meposhtem shfaq passwordin kur simboli i syrit mbahet i klikuar
    **/
    function shfaqPass() {
      document.getElementById("Password").type = "text";
    }

    /**
     * Funksioni i meposhtem fsheh passwordin kur simboli i syrit nuk klikohet me
    **/
    function fshihPass() {
    document.getElementById("Password").type = "password";
    }


    /**
    * Funksioni i meposhtem shkarkon dokumentat e formatit word dhe excel
    **/
    function shkarkoDokumentinWE(obj) {
    var id = obj;
    $("#previewDok").html("<iframe id='viewDoc' src='/Home/ShikoDokumentWordExcel?dID=" + id + "'></iframe>");
    }


    /**
    * Funksioni i meposhtem aktivizon butonin e shkarkimit nese ka te pakten nje element te perzgjedhur
     **/
    function aktivizoShkarkim() {

        var nr = countTeZgjedhur();
        if (nr > 0) {
            document.getElementById("btnShkarko").disabled = false;
        }
        else {
            document.getElementById("btnShkarko").disabled = true;
        }
    }

    /**
    * Funksioni i meposhtem numeron se sa elemente (dokumenta/foldera) jane te zgjedhur
    **/
    function countTeZgjedhur() {
        var nr = 0;
        var i = 0;
        var allcheckboxes = document.getElementsByClassName("btnTipDok");
         while ((nr < 2) && (i < allcheckboxes.length)) {
            if (allcheckboxes[i].checked == true) {
                nr = nr + 1;
            }
            i = i + 1;
         }
                return nr;
     }

    /**
    * Funksioni i meposhtem zgjedh folderin e klikuar dhe aktivizon shkarkimin nese jane perzgjedhur nje ose me shume
    **/
    function Zgjidh(obje) {

        if ($(obje).parent().parent().children().first().children().first().prop("checked") == false) {
            $(obje).parent().parent().children().first().children().first().prop('checked', true);
        }
        else {
            $(obje).parent().parent().children().first().children().first().prop('checked', false);
        }
        var nr = countTeZgjedhur();
        if (nr > 0) {
            document.getElementById("btnShkarko").disabled = false;
        }
        else {
            document.getElementById("btnShkarko").disabled = true;
        }

    }

    /**
    * Funksioni i meposhtem zgjedh dokumentin e klikuar, aktivizon shkarkimin nese jane perzgjedhur nje ose me shume dhe nese eshte i vetem, aktivizon butonin e fshirjes
    **/
    function ZgjidhDokument(obje) {
    var dokID;
        if ($(obje).is("input") == false) {
            if ($(obje).parent().parent().children().first().children().first().prop("checked") == false) {
            $(obje).parent().parent().children().first().children().first().prop('checked', true);
            }
            else {
            $(obje).parent().parent().children().first().children().first().prop('checked', false);
            }
        };

        var nr = countTeZgjedhur();
        if (nr > 0) {
            document.getElementById("btnShkarko").disabled = false;
        }
        else {
            document.getElementById("btnShkarko").disabled = true;
        }
    if (nr == 1) {
        var abc = document.getElementsByClassName("checkbox-1");
        for (var ind = 0; ind < abc.length; ind++) {
            if (abc[ind].checked == true) {
                dokID = abc[ind].id.replace('checkbox-', '');
                break;
            }
        }

        $.ajax({
            url: '/Home/GetTeDhenaDokumenti',
            data: { 'id': dokID },
            type: "post",
            success: function (result) {
                $('#ModalShikoDokument').html(result);
            },
        });

        document.getElementById("btnFshi").disabled = false;
        document.getElementById("btnShiko").disabled = false;
    }

        else {
            document.getElementById("btnFshi").disabled = true;
            document.getElementById("btnShiko").disabled = true;
    }


}


    function changePreview(id) {

    $.ajax({
        url: '/Home/GetTeDhenaDokumenti',
        data: { 'id': id },
        type: "post",
        success: function (result) {
            $('#ModalShikoDokument').html(result);
        },
    });


}

    /**
    * Funksioni i meposhtem fshin nga forma e filtrimit, llojin e klikuar, i cili eshte perzgjedhur me pare,
    **/
    function HiqLlojin(obj) {
        var nrLlojesh = $(obj).parent().parent().children().length;
        if (nrLlojesh < 2) {

            $(obj).parent().parent().css("display", "none");
        }


        var s = document.getElementById("teZgjedhura").value;
        var replacee = $(obj).parent().attr("id") + "/";
        s = s.replace(replacee, "");
        document.getElementById("teZgjedhura").value = s;
        $(obj).parent().remove();


    }

    /**
    * Funksioni i meposhtem shton ne formen e filtrimit llojin e klikuar, pra qe eshte perzgjedhur
    **/
    function ShtoLlojin() {
            document.getElementById("LlojetFilter").style.display = "inherit";
        var selIndex = document.getElementById("llojDok").selectedIndex;
        var txt = document.getElementById("llojDok").getElementsByTagName("option")[selIndex].text;
        var ndodhet = false;
        var llojet = document.getElementsByClassName("lloje");
        if (llojet.length > 0) {
            for (var i = 0; i < llojet.length; i++){
                if (llojet[i].textContent.includes(txt)) {
            ndodhet = true;
                    break;
                }
            }
        }

        if ((ndodhet == false) && (txt.includes("Zgjidh") == false)) {
            document.getElementById("LlojetFilter").innerHTML += "<label id='" + txt + "' class='d-block lloje'>" + txt + "<span class='x' onclick='HiqLlojin(this)'> X</span></label>";
            document.getElementById("teZgjedhura").value += txt;
            document.getElementById("teZgjedhura").value += "/";
        }


        setTimeout(() => {

            document.getElementById("llojDok").getElementsByTagName("option")[0].selected = "selected";
        }, 200);


    }

    /**
    * Funksioni i meposhtem pastron fushat e filtrimit ne klikim te butonit "Pastro" dhe kthen te gjithe subjektet
    **/
    function PastroFushat() {
        var inputs = document.getElementById("divfilter").getElementsByTagName("input");
        for (var i = 0; i < inputs.length; i++) {
            inputs[i].value = "";
        }
        document.getElementById("llojDok").getElementsByTagName("option")[0].selected = "selected";
        document.getElementById("LlojetFilter").innerHTML = "";
        document.getElementById("lblError").innerHTML = "";
        document.getElementById("LlojetFilter").style.display = "none";


        $.ajax({
            url: '/Home/ShfaqFolderat',
            type: "post",
            data: { 'ElemId': 0, 'niv': -1 },
            success: function (result) {
                $('#divFolderat').html(result);
            }
        });
    }

    /**
     * Funksioni i meposhtem lejon vetem shkronja dhe numra
    **/
    function OnlyLettersAndNumbers() {
    alert("nrlett");
    return /[a-zA-Z0-9-_ ]/.test(event.key)
    }

    /**
    * Funksioni i meposhtem shton fushat ne formen e krijimit te dokumentit
    **/
    function shtoFushe() {
        var a = document.getElementById("inpFusha").value.trim();
        var ekziston = false;
        if (a.length > 0) {
            var fushaaktuale = document.getElementById("divFushat").getElementsByClassName("fusha-inline");
            for (i = 0; i < fushaaktuale.length; i++)
            {
                if (fushaaktuale[i].textContent.substring(0, fushaaktuale[i].textContent.length - 3) == a) {
            ekziston = true;
                    break;
                }
            }
            if (ekziston == false) {
            document.getElementById("divFushat").style.display = "block";
                var str = "<div class='fusha-inline'>" + a + " <label class='x' onclick='Hiq(this)'> X</label><br></div>  ";
                document.getElementById("divFushat").innerHTML += str;
                document.getElementById("fushaTeZgjedhura").value += a + ",";
            }

            document.getElementById("inpFusha").value = "";
        }
    }

    /**
    * Funksioni i meposhtem shton fusha te reja ne preview nqs nuk ekzistojne
    **/
    function shtoFusheTeRe() {
                    var eshte = false;
                    var fusha = document.getElementById("txtFusheERe").value.trim();
                    var fushaTeReja = document.getElementsByClassName("f-e-re");
                    var fushaEkzistuese = document.getElementById("prev-fusha").getElementsByClassName("col-sm-4");
        for (var i = 0; i < fushaTeReja.length; i++)
            {
              if(((fushaTeReja[i].textContent).substring(0, fushaTeReja[i].textContent.length - 2)) == fusha)
                    {
                            eshte = true;
                            break;
                     }
                     }
                                    for (var j = 0; j < fushaEkzistuese.length; j++) {
                                        if (((fushaEkzistuese[j].textContent).substring(0, fushaEkzistuese[j].textContent.length - 2)) == fusha) {
        eshte = true;
                                            break;
                                        }
                                    }

                                    if (eshte == false) {


                                        var elem = "<span class='col-sm-4 f-e-re'>" + fusha + "<span id='" + fusha + "' class='text-danger fshirje' onclick=\"HiqFusheDokR('"+fusha+"')\"> X</span></span>";

                                        document.getElementById("prev-fusha-re").innerHTML += elem;

                                    }
                                    document.getElementById("txtFusheERe").value = "";
                                }

    /**
    * Funksioni i meposhtem heq fusha nga preview
    **/
    function HiqFusheDok(fusha) {
        document.getElementById(fusha).parentNode.remove();
        var elem = "<span>" + fusha + "</span>";
        document.getElementById("Hequr").innerHTML += elem;
    }

    /**
    * Funksioni i meposhtem heq elementin e perzgjedhur ne fushat "Ngarko dokumentat", "Fusha Indeksimi" per shtimin e nje dokumenti.
    **/
    function Hiq(obj) {
        var txt = $(obj).parent().text();
        var s = document.getElementById("fushaTeZgjedhura").value;
        txt = txt.substring(0, txt.length - 3);
        txt += ",";
        s = s.replace(txt, "");
        document.getElementById("fushaTeZgjedhura").value = s;
        var elem = $(obj).parent().parent().children().length;
        if (elem < 2) {
                $(obj).parent().parent().css("display", "none");
        }
        $(obj).parent().remove();
    }


    /**
    * Funksioni i meposhtem zgjedh te gjithe folderat/filet e shfaqura ne folderin qe eshte i hapur kur klikohet butoni "zgjidh te gjitha" dhe aktivizon butonat perkates
    **/
    function ZgjidhTeGjitha() {
        var allselected = true;
        var i;
        var SelID;
        var counter = 0;
        var checknr = document.getElementsByTagName("input");
        for (var chi = 0; chi < checknr.length; chi++) {
            if ((checknr[chi].type == "checkbox") && (checknr[chi].classList.contains("dok")))
                counter++;
            SelID = checknr[chi].id.replace('checkbox-', '');
        }
        if (counter == 1) { //nqs eshte vetem nje dokument i selektuar aktivizohet fshirja dhe preview e tij

                document.getElementById("btnShiko").disabled = false;

                document.getElementById("btnFshi").disabled = false;
                $.ajax({
                    url: '/Home/GetTeDhenaDokumenti',
                    data: { 'id': SelID },
                    type: "post",
                    success: function (result) {
                        $('#ModalShikoDokument').html(result);
                    },
                });
            }
            else {
                document.getElementById("btnShiko").disabled = true;

                document.getElementById("btnFshi").disabled = true;
            }
        var allcheckboxes = document.getElementsByClassName("btnTipDok");
        for (i = 0; i < allcheckboxes.length; i++) {
            if (allcheckboxes[i].checked == false) {
                allselected = false;
                break;
            }
        }

        if (allselected == true) {

            for (var j = 0; j < allcheckboxes.length; j++) {
                allcheckboxes[j].checked = false;
            }
            document.getElementById("btnShkarko").disabled = true;
            document.getElementById("btnFshi").disabled = true;
            document.getElementById("btnShiko").disabled = true;

        }
        else {
            for (var j = 0; j < allcheckboxes.length; j++) {
                allcheckboxes[j].checked = true;

                document.getElementById("btnShkarko").disabled = false;
            }


            }
    }

    /**
    * Funksioni i meposhtem kontrollon dokumentat/folderat e selektuar dhe therret metoden perkatese ne kontroller per shkarkimin e tyre
    **/
    function shkarkoDokumenta() {
    var nrSelektuar = countTeZgjedhur();
    if (nrSelektuar == 1) {
        var dokID;
        var checkboxe = document.getElementsByClassName("checkbox-1");
        for (var ind = 0; ind < checkboxe.length; ind++) {
            if (checkboxe[ind].checked == true) {
                dokID = checkboxe[ind].id.replace('checkbox-', '');
                break;
            }
        }
        if (document.getElementById("checkbox-" + dokID).getAttribute("class").includes("dok") == true) { //selektuar vetem 1 dokument, shkarkohet dokumenti
            $("#docs").html("<a id='downlDoc' href='/Home/ShkarkoDokument?id=" + dokID + "'></a>");
            $("#downlDoc")[0].click();
        }
        else {
            if (document.getElementById("checkbox-" + dokID).getAttribute("class").includes("lloj") == true) {
                var inspID = document.getElementById("checkbox-" + dokID).getAttribute("class").replace("btnTipDok checkbox-1 lloj lloj-", "");
                $("#docs").html("<a id='downlDoc' href='/Home/ShkarkoFolderZIP?tipi=2&idll=" + dokID + "&idi=" + inspID + "&ids=0'></a>"); //shkarkon folder zip ne varesi te inspektimit dhe llojit te dokumentit
                $("#downlDoc")[0].click();
            }
            else if (document.getElementById("checkbox-" + dokID).getAttribute("class").includes("inspektim") == true) {
                $("#docs").html("<a id='downlDoc' href='/Home/ShkarkoFolderZIP?tipi=1&idll=0&idi=" + dokID + "&ids=0'></a>");//shkarkon folder zip sipas inspektimit
                $("#downlDoc")[0].click();
            }
            else if (document.getElementById("checkbox-" + dokID).getAttribute("class").includes("subjekt") == true) {
                $("#docs").html("<a id='downlDoc' href='/Home/ShkarkoFolderZIP?tipi=0&idll=0&idi=0&ids=" + dokID + "'></a>");//shkarkon folder zip sipas subjektit
                $("#downlDoc")[0].click();
            }

        }
    }
    else { //kur jane selektuar disa dokumenta
        var doks = new Array();
        var i = 0;
        var checkboxe = document.getElementsByClassName("checkbox-1");
        for (var ind = 0; ind < checkboxe.length; ind++) {
            if (checkboxe[ind].checked == true) {
                dokID = checkboxe[ind].id.replace('checkbox-', '');

                doks[i] = dokID;

                i++;
            }

        }
        if (document.getElementById("checkbox-" + doks[0]).getAttribute("class").includes("dok") == true) {

            var elem = "";
            for (var j = 0; j < doks.length; j++) {
                elem = elem + "dokID=" + doks[j] + "&";
            }
            elem = elem.substring(0, elem.length - 1);

            $("#docs").html("<a id='downlDoc' href='/Home/ShkarkoDokumenta?" + elem + "'></a>");
            $("#downlDoc")[0].click();
        }
        else if (document.getElementById("checkbox-" + doks[0]).getAttribute("class").includes("subjekt") == true) {

            var elem = "";
            for (var j = 0; j < doks.length; j++) {
                elem = elem + "subID=" + doks[j] + "&";
            }
            elem = elem.substring(0, elem.length - 1);

            $("#docs").html("<a id='downlDoc' href='/Home/ShkarkoSubjekte?" + elem + "'></a>");
            $("#downlDoc")[0].click();
        }
        else if (document.getElementById("checkbox-" + doks[0]).getAttribute("class").includes("inspektim") == true) {

            var elem = "";
            for (var j = 0; j < doks.length; j++) {
                elem = elem + "insID=" + doks[j] + "&";
            }
            elem = elem.substring(0, elem.length - 1);

            $("#docs").html("<a id='downlDoc' href='/Home/ShkarkoInspektime?" + elem + "'></a>");
            $("#downlDoc")[0].click();
        }
        else if (document.getElementById("checkbox-" + doks[0]).getAttribute("class").includes("lloj") == true) {

            var elem = "";
            for (var j = 0; j < doks.length; j++) {
                elem = elem + "llojID=" + doks[j] + "&";
            }
            elem = elem.substring(0, elem.length - 1);
            var inspID = document.getElementById("checkbox-" + doks[0]).getAttribute("class").replace("btnTipDok checkbox-1 lloj lloj-", "");
            $("#docs").html("<a id='downlDoc' href='/Home/ShkarkoLloje?insID=" + inspID+"&" + elem + "'></a>");
            $("#downlDoc")[0].click();
        }
        else {
            alert("jo dok");
        }
    }
}

    /**
    * Funksioni i meposhtem krahason datat qe shkruan perdoruesi ne filter. Nese data e fillimit eshte me e madhe se ajo e mbarimit, fushat fshihen qe perdoruesi te fuse nje date te sakte
    **/
    function krahasoDatat() {
    var dt1 = document.getElementById("data1").value;
    var dt2 = document.getElementById("data2").value;
    if ((dt1 == "") || (dt2 == "")) {
        $('#lblError').html("");
    }
    else {
           var data1 = dt1.split(".");
            var data2 = dt2.split(".");
            var d1 = new Date(data1[2], data1[1], data1[0]);
            var d2 = new Date(data2[2], data2[1], data2[0]);

            if (d2 < d1) {
                $('#lblError').html("Data e fillimit duhet të jetë më e vogël se ajo e mbarimit");
                document.getElementById("data1").value = "";
                document.getElementById("data2").value = "";
            }
            else {
                $('#lblError').html("");
            }
       }

}

    /**
    * Funksioni i meposhtem caktivizon butonin e fshirjes, shkarkimit dhe shikimit
    **/
    function caktivizoFshirje() {
        document.getElementById("btnFshi").disabled = true;
        document.getElementById("btnShiko").disabled = true;
        document.getElementById("btnShkarko").disabled = true;
    }

    /**
    * Funksioni i meposhtem ruan ne db ndryshimet per nje dokument te shfaqur ne preview
    **/
    function ruajNdryshimet(id) {

    var z = document.getElementById("prev-z").value;
    var k = document.getElementById("prev-k").value;
    var r = document.getElementById("prev-r").value;

    var fushatHeqjeSpans = document.getElementById("Hequr").getElementsByTagName("span");
    var fushatHeqje = [];
    for (var i = 0; i < fushatHeqjeSpans.length; i++) {
        fushatHeqje[i]=fushatHeqjeSpans[i].textContent;
    }

    var fushatShtimSpans = document.getElementById("prev-fusha-re").getElementsByClassName("f-e-re");
    var fushatShtim = [];
    for (var j = 0; j < fushatShtimSpans.length; j++) {


        fushatShtim[j] = fushatShtimSpans[j].textContent.substring(0, fushatShtimSpans[j].textContent.length - 2);

    }

    $.ajax({
        url: '/Home/UpdateDokument',
        type: "post",
        data: { 'zyra': z, 'rafti': r, 'kutia':k, 'id':id, 'fushatShtim':fushatShtim, 'fushatHeqje':fushatHeqje },
        success: function (result) {
            $('#divFolderat').html(result);
            document.getElementById("close-button").click();
            document.getElementById("alertModal").style.display = "block";

        }
    });
}


    /**
    * Funksioni i meposhtem heq nje fushe nga fushat e reja te shtuara ne preview
    **/
    function HiqFusheDokR(fusha) {
        document.getElementById(fusha).parentNode.remove();
    }


    /**
     * Funksioni i meposhtem hap folderin ne doubleclick
     **/
    function KlikoAjax(obj) {
        var id = $(obj).parent().find("a").first().text();
        document.getElementById(id).click();
    }

    /**
    * Funksioni i meposhtem mbyll modalin e mesazhit per perditesimin e dokumentit
    **/
    function mbyllMesazh() {
        document.getElementById("alertModal").style.display = "none";
    }

    /**
     * Funksioni i meposhtem mbyll modalin e mesazhit per fshirjen e dokumentit
     **/
    function mbyllMesazhFshirjeje() {
        document.getElementById("alertModalFshirje").style.display = "none";
    }

    /**
     * Funksioni i meposhtem mbyll modalin e mesazhit per fshirjen e dokumentit
     **/
    function mbyllMesazhKonfirmimi() {
        document.getElementById("alertModalKonfirmim").style.display = "none";
    }


/**
 * Funksioni i meposhtem pastron fushat e shtimit te dokumentit
 **/
function PastroTeGjithaFushat() {
       var perPastrim = document.getElementsByClassName("pas");
        for (var i = 0; i < perPastrim.length; i++) {

            perPastrim[i].value = "";
    }
    document.getElementById("selectedFiles").innerHTML = "";
    document.getElementsByClassName("lloj-pas")[0].getElementsByTagName("option")[0].selected = "selected";



}


