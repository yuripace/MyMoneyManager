<?php
if ($_POST) {
    inserisci_record();
} else {
    mostra_form();
}

function inserisci_record()
{
    // richiamo lo script responsabile della connessione a MySQL
    require 'connect.php';
    
    // recupero i campi di tipo "stringa"
    $cognome = trim($_POST['cognome']);
    $nome = trim($_POST['nome']);
    $societa = trim($_POST['societa']);
    
    $dataentr = $_POST["dtaIngresso"];
    $oraent = $_POST["oraIngresso"];
    $orausc = $_POST["oraUscita"];
    $badge = $_POST["badge"];
    
    $pieces = explode("-", $dataentr);
    echo $pieces[0]; // mese
    echo $pieces[1]; // giorno
    
    if (strlen($pieces[0]) < 2) {
        $pieces[0] = "0" . $pieces[0];
    }
    
    if (strlen($pieces[1]) < 2) {
        $pieces[1] = "0" . $pieces[1];
    }
    
    $data = $pieces[2] . "-" . $pieces[1] . "-" . $pieces[0];
    // verifico se devo eliminare gli slash inseriti automaticamente da PHP
    if (get_magic_quotes_gpc()) {
        $cognome = stripslashes($cognome);
        $nome = stripslashes($nome);
        $societa = stripslashes($societa);
    }
    
    $cognome = mysql_real_escape_string($cognome);
    $nome = mysql_real_escape_string($nome);
    $societa = mysql_real_escape_string($societa);
    
    /*
     * $sesso = isset($_POST['sesso']) ? intval($_POST['sesso']) : 0;
     * $newsletter = isset($_POST['newsletter']) ? 1 : 0;
     * $attivita = intval($_POST['attivita']);
     *
     * // verifico la presenza dei campi obbligatori
     * if(!$nome)
     * {
     * $messaggio = urlencode("Non hai inserito il nome");
     * header('location: '.$_SERVER['PHP_SELF'].'?msg='.$messaggio);
     * exit;
     * }
     */
    // preparo la query
    $query = "INSERT INTO ElencoAccessi(Cognome, Nome, Societa, DataEntrata, OraEntrata, OraUscita, Badge) 
			  VALUES ('$cognome','$nome','$societa','$data','$oraent','$orausc','$badge')";
    
    if (isset($_POST['id'])) {
        $idd = $_POST['id'];
        if (is_numeric($idd)) {
            $query = "UPDATE ElencoAccessi SET Cognome='$cognome', Nome='$nome', Societa='$societa', DataEntrata='$data', OraEntrata='$oraent', OraUscita='$orausc', Badge='$badge'  WHERE ID= '$idd'";
        }
    }
    
    // invio la query
    $result = mysql_query($query);
    
    // controllo l'esito
    if (! $result) {
        die("Errore nella query $query: " . mysql_error());
    }
    
    // recupero l'id autoincrement generato da MySQL per il nuovorecord inserito
    $id_inserito = mysql_insert_id();
    
    // chiudo la connessione a MySQL
    mysql_close();
    
    $messaggio = urlencode("Inserimento effettuato con successo (Data=$data)");
    // header('location: '.$_SERVER['PHP_SELF'].'?msg='.$messaggio);
    header('location: ../html/accessi/elenco_accessi2.html');
}

function mostra_form()
{
    // mostro un eventuale messaggio
    if (isset($_GET['msg']))
        echo '<b>' . htmlentities($_GET['msg']) . '</b><br /><br />';
    ?>
	

	<?php
}
?>