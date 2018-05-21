<?php
require 'connect.php';
$query = 'SELECT * FROM ElencoAccessi';
$result = mysql_query($query);

// controllo l'esito
if (! $result) {
    die("Errore nella query $query: " . mysql_error());
}

if (0 == mysql_num_rows($result)) {
    //echo 'Nessun accesso rilevato';
} else {
    while ($row = mysql_fetch_array($result)) {
        $id = $row['ID'];
        
        $records["data"][] = array(
            
            $id,
	  htmlspecialchars($row['Badge']),
      htmlspecialchars($row['Cognome']),
      htmlspecialchars($row['Nome']),
      htmlspecialchars($row['Societa']),
      htmlspecialchars($row['DataEntrata']),
	  htmlspecialchars($row['OraEntrata']),
	  htmlspecialchars($row['OraUscita']),
	  '<a href="nuovo_accesso2.html?ID='.$id.'"><i class="fa fa-pencil"></i></a>'
            
        );
    }
}

// recupero l'id autoincrement generato da MySQL per il nuovorecord inserito
	$id_inserito = mysql_insert_id();

// chiudo la connessione a MySQL
	mysql_close();
	if(isset($records))
	{
		echo json_encode($records);
	}
	else{
		$records["data"][] = null;
		echo json_encode($records);
	}
?>