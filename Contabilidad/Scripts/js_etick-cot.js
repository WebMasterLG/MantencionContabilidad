$(document).ready(function(){ 
  //Signin Contraseña
    $('#show-pass').click(function () {
      if ($('#inputPassword').attr('type') === 'password') {
        $('#inputPassword').attr('type', 'text');
        $('#ver').show();
        $('#nover').hide();
      } else {
        $('#inputPassword').attr('type', 'password');
        $('#ver').hide();
        $('#nover').show();
      }
    });
    //Mostrar otra relacion con propiedad
    $("#Propietario3").click(function(){
      $("#OtraRelac").show();
    });
    $("#Propietario1").click(function(){
      $("#OtraRelac").hide();
    });
    $("#Propietario2").click(function(){
      $("#OtraRelac").hide();
    });
  //Form Propiedades
  $("#selectProyecto").change(function(){
    var valor = $(this).val();
    if (valor=='15') {
     $("#inputDireccion").val('Calle Nueva Esperanza');
     $("#inputNCasa").val('111');
     $("#selectInmobiliaria option[value="+ 30 +"]").attr("selected",true);
     $("#selectRegion option[value="+ 13 +"]").attr("selected",true);
     $("#selectComuna option[value="+ 11 +"]").attr("selected",true);
     $("#inputMetro").val('Santa Lucía');
     $("#TipoBien option[value="+ 1 +"]").attr("selected", true);
   } else{
     $("#inputDireccion").val('Dirección');
     $("#inputNCasa").val('Número');
     $("#selectInmobiliaria option[value="+ 0 +"]").attr("selected",true);
     $("#selectRegion option[value="+ 0 +"]").attr("selected",true);
     $("#selectComuna option[value="+ 0 +"]").attr("selected",true);
     $("#inputMetro").val('Metro');
     $("#TipoBien option[value="+ 0 +"]").attr("selected", true);
   }
  });
  //Otra cuenta deposito
  $(function(){
    $('#OtraCuentaCheck').change(function(){
      if(!$(this).prop('checked')){
        $('#DatoOtraC').show();
      }else{
        $('#DatoOtraC').hide();
      }
    })
  });
  $(function(){
    $('#OtraCuentaCheckArr').change(function(){
      if(!$(this).prop('checked')){
        $('#DatoOtraCArr').show();
      }else{
        $('#DatoOtraCArr').hide();
      }
    })
  });
  //Tooltip
  $(function () {
    $('[data-toggle="tooltip"]').tooltip()
  });
  //Generar link
  $("#GENLink").click(function(){
      $(".copy-link").show();
      $(".rrss").show();
    });
  //Generar Salvoconducto
  $("#GenSalCon").click(function(){
      $("#GenSCOK").show();
      $("#GenSalCon").hide();
    });
  //Generar Acta entrega
  $("#GenActaEnt").click(function(){
      $("#GenAE").show();
      $("#GenActaEnt").hide();
    });

  //Calendarios
    $( function() {
      $("#FEI").datepicker();
      $("#FPV").datepicker();
      $("#FEE").datepicker();
      $("#FR").datepicker();
      $("#PDE").datepicker();
      $('#FISBs').datepicker();
      $('#FTSBs').datepicker();
      $('#FESCR').datepicker();
      $('#FEIArr').datepicker();
      $('#FPVArr').datepicker();
      $('#FEEArr').datepicker();
      $('#FRArr').datepicker();
      $('#FDesc').datepicker();
      $('#FdN').datepicker();
      $('#FDRA').datepicker();
      $('#FIniCont').datepicker();
      $('#FTermCont').datepicker();
      $('#FGPV').datepicker();
      $('#FGPP').datepicker();
      $('#FRCC').datepicker();
      $('#FPD').datepicker();
      $('#FPR').datepicker();
      $('#FPH').datepicker();
      $('#FFA').datepicker();
      $('#FFA1').datepicker();
      $('#FDEnv').datepicker();
      $('#FDEnvSc').datepicker();
      $('#FDEnvEt').datepicker();
      $('#GarFI').datepicker();
      $('#GarFT').datepicker();
      $('#GarFI1').datepicker();
      $('#GarFT1').datepicker();
      $('#GarSD').datepicker();
      $('#GarCD').datepicker();
      $('#GarDP').datepicker();
      $('#GarECD').datepicker();
      $('#DateTable').datepicker();
      $('#DateTable1').datepicker();
      $('#DateTable2').datepicker();
      $('#DateTable3').datepicker();
      $('#DateTable4').datepicker();
      $('#DateTable5').datepicker();
      $('#DateTable6').datepicker();
      $('#DateTable7').datepicker();
      $('#DateTablea').datepicker();
      $('#DateTableb').datepicker();
      $('#DateTablec').datepicker();
      $('#DateTabled').datepicker();
      $('#DateTablee').datepicker();
      $('#DateTable1a').datepicker();
      $('#DateTable1b').datepicker();
      $('#DateTable1c').datepicker();
      $('#DateTable2a').datepicker();
    });
    //suma metros
    var res
    $('#sum').on('keyup', function(){
      var valor = $('#MU').val();
      var valor2 = $('#MT').val();
      res = (parseInt(valor) + parseInt(valor2));
      $('#MTL').val(res);
      if (res > 139) {  
        $("#DFL").prop("checked", true);
      }else{
        $('#DFL').prop("checked",false);
      }
    });
       //suma descuentos
    var res
    $('#MontDesc').on('keyup', function(){
      var valor = $('#DescInv').val();
      var valor2 = $('#DescArr').val();
      res =(parseInt(valor) + parseInt(valor2));
      $('#DescTot').val(res);
    });
    //up
    var offset = 200;
    var duration = 500;
    $('.up').on('click',function(event) {
      event.preventDefault();
      $('html, body').animate({
        scrollTop: 0
      }, 600);
      return false;
    });
    //Una Persona natural
    $("#Propietario1").click(function(){
      $("#1Per").show();
      $("#2Per").hide();
      $("#3Per").hide();
    });
    //Dos Persona natural
    $("#Propietario2").click(function(){
      $("#1Per").show();
      $("#2Per").show();
      $("#3Per").hide();
    });
    //Una Persona natural + empresa
    $("#Propietario3").click(function(){
      $("#1Per").show();
      $("#3Per").show();
      $("#2Per").hide();
      $('.Em').show();
    });
    //Empresa
    $("#PropietarioE").click(function(){
      $("#3Per").show();
      $("#1Per").hide();
      $("#2Per").hide();
      $('.Em').hide();
    });
    //Una Persona natural ARRIENDO
    $("#Propietario1Arr").click(function(){
      $("#1PerArr").show();
      $("#2PerArr").hide();
      $("#3PerArr").hide();
    });
    //Dos Persona natural ARRIENDO
    $("#Propietario2Arr").click(function(){
      $("#1PerArr").show();
      $("#2PerArr").show();
      $("#3PerArr").hide();
    });
    //Una Persona natural + empresa ARRIENDO
    $("#Propietario3Arr").click(function(){
      $("#1PerArr").show();
      $("#3PerArr").show();
      $("#2PerArr").hide();
    });
    //Empresa ARRIENDO
    $("#PropietarioEArr").click(function(){
      $("#3PerArr").show();
      $("#1PerArr").hide();
      $("#2PerArr").hide();
      $('.EmArr').hide();
    });
    // Avales
    $(function(){
      $('#AvalArr').change(function(){
        if(!$(this).prop('checked')){
          $('#PerAval').show();
          $('#AvalArr2').show();
        }else{
          $('#PerAval').hide();
          $('#AvalArr2').hide();
        }
      })
    });
    $(function(){
      $('#AvalArr2').change(function(){
        if(!$(this).prop('checked')){
          $('#PerAval2').show();
        }else{
          $('#PerAval2').hide();
        }
      })
    });
  $( ".menu-toggle" ).click(function() {
    $(".menu-toggle").toggleClass('open');
    $(".menu-line").toggleClass('open');
  });
  $('#lookP').click(function () {
    $('#nav-New-tab').show();
    $('#nav-New-tab').addClass('active');
    $('nav-New').show();
    $('#nav-New').addClass('active show');
    $('#nav-publicada-tab').removeClass('active');
    $('#nav-publicada').removeClass('active show');
  });
  $('.closeTab').click(function(){
    $('#nav-New-tab').css('display' , 'none');
    $('#nav-New').css('display' , 'none');
    $('#nav-publicada-tab').addClass('active');
    $('#nav-publicada').addClass('active show');
    });
  //Validaciones y botones
    $("#nav-ValRenov-tab").click(function(){
      $('#btnOK').css('display' , 'block');
      $('#btnOK2').css('display' , 'none');
      $('#btnOK3').css('display' , 'none');
      $('#btnOK4').css('display' , 'none');
    });
    $("#nav-NOdispon-tab").click(function(){
      $('#btnOK').css('display' , 'block');
      $('#btnOK2').css('display' , 'none');
      $('#btnOK3').css('display' , 'none');
      $('#btnOK4').css('display' , 'none');
    });
    
    $("#nav-CheckArriendo-tab").click(function(){
      $('#btnOK').css('display' , 'block');
      $('#btnOK2').css('display' , 'none');
      $('#btnOK3').css('display' , 'none');
      $('#btnOK4').css('display' , 'none');
    });
    $("#nav-CheckFinanzas-tab").click(function(){
      $('#btnOK2').css('display' , 'block');
      $('#btnOK').css('display' , 'none');
      $('#btnOK3').css('display' , 'none');
      $('#btnOK4').css('display' , 'none');
    });
    $("#nav-FirmGer-tab").click(function(){
      $('#btnOK3').css('display' , 'block');
      $('#btnOK').css('display' , 'none');
      $('#btnOK2').css('display' , 'none');
      $('#btnOK4').css('display' , 'none');
      $('#OtroBtns').css('display' , 'none');
    });
    $("#nav-CheckNotaria-tab").click(function(){
      $('#btnOK4').css('display' , 'block');
      $('#btnOK').css('display' , 'none');
      $('#btnOK2').css('display' , 'none');
      $('#btnOK3').css('display' , 'none');
    });
    $("#nav-SDevol-tab").click(function(){
      $('#OtroBtns').css('display' , 'block');
    });
    $("#nav-AprobDevol-tab").click(function(){
      $('#OtroBtns').css('display' , 'block');
    });
    $("#nav-FDevol-tab").click(function(){
      $('#OtroBtns').css('display' , 'block');
    });
    //Finanzas
    $("#nav-cotFact-tab").click(function(){
      $('#btn-Facturado').css('display' , 'none');
    });
    $("#nav-cotTrue-tab").click(function(){
      $('#btn-Facturado').css('display' , 'block');
    });
    $("#SearchFolio").change(function(){
    var valor = $(this).val();
    if (valor=='5556') {
     $("#SearchDireccion").val(' ');
     $("#SearchPropietario").val(' ');
   } else{
     $("#SearchDireccion").val('Artemio Gutierres 1680. Depto. 2');
     $("#SearchPropietario").val('Aaron Garay Sobarzo');
     $(".numCot").append('N° 35');
     $("#accordionCotizaciones").show();
   }
  });
  //Add uno
  $('#AddMas').click(function() {
  var item = document.getElementById("CaractPropd").value;
  var DItem = document.getElementById("DescripItem").value;
  var i = 1; //contador para asignar id al boton que borrara la fila
  var fila = '<tr class="item" id="row' + i + '"><td class="text-left" colspan="3"><b>' + item + ':' + '</b></td><td><b>' + DItem + '</b></td><td colspan="5"></td><td><button type="button" name="remove" id="' + i + '" class="USL-1 btn_remove"><i class="fas fa-trash mt-2" data-toggle="tooltip" data-placement="top" title="Eliminar"></i></button></td></tr><tr><td><select class="form-control" id="DetObra"><option selected="selected" hidden="">Detalle de obra</option><option>Mano de Obra externa</option><option>Mano de Obra interna</option><option>Material Directo</option><option>Material Indirecto</option><option>Movilización (bip-combustible-taxi)</option><option>Test 3</option></select></td><td><input type="text" class="form-control" placeholder="Descripción" id="DetDesc"></td><td><input type="number" class="form-control" id="DelCant"></td><td><input type="number" class="form-control" id="DetCU"></td><td>50.000</td><td class="row"><select class="form-control mr-2 col-md-5" id="DetTipo"><option>$</option><option>%</option></select><input type="number" class="form-control col-md-4"></td><td>52.100</td><td>8.999</td><td>61.999</td><td><button class="color-corp" id="AddMas2" type="button"><i class="fas fa-plus-circle" data-toggle="tooltip" data-placement="right" title="" data-original-title="agregar"></i></button></td></tr>'; //esto seria lo que contendria la fila

  $('#CotFinal #CotFtbody').after(fila);
    var nFilas = $("#CotFinal tr").length;
    //le resto 1 para no contar la fila del header
    document.getElementById("DescripItem").value ="";
    document.getElementById("CaractPropd").value = "";
    document.getElementById("CaractPropd").focus();
  });
$(document).on('click', '.btn_remove', function() {
  var button_id = $(this).attr("id");
    //cuando da click obtenemos el id del boton
    $('#row' + button_id + '').remove(); //borra la fila
    //limpia el para que vuelva a contar las filas de la tabla
    var nFilas = $("#mytable tr").length;
  });

});