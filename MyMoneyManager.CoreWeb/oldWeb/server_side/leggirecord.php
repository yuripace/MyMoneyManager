<?php
  /* 
   * Paging
   */

   if (isset($_POST['id'])) {

        
        require 'connect.php';
			$query='SELECT * FROM ElencoAccessi where ID=' .$_POST['id'];
			$result = mysql_query($query);

			// controllo l'esito
			if (!$result) {
				die("Errore nella query $query: " . mysql_error());		
			}

			if(0 == mysql_num_rows($result)) {
			echo 'Nessun accesso rilevato';
		}
		else { 
			while($row = mysql_fetch_array($result)) {
			   $id = $row['ID'];
			   $dataentrata=htmlspecialchars($row['DataEntrata']);
			   $date=date_create($dataentrata);
			   $dataentrata=date_format($date,"d-m-Y");
			   $orae=htmlspecialchars($row['OraEntrata']);
			   $pieces = explode(":", $orae);
			   $orae=$pieces[0].":".$pieces[1];
			   $orau=htmlspecialchars($row['OraUscita']);
			   $pieces = explode(":", $orau);
			   $orau=$pieces[0].":".$pieces[1];			   
			   $ar = array($id, htmlspecialchars($row['Cognome']),htmlspecialchars($row['Nome']),  htmlspecialchars($row['Societa']),$dataentrata ,$orae ,$orau, htmlspecialchars($row['Badge']));
			}
        }
		mysql_close();

   


    
} 
	// recupero l'id autoincrement generato da MySQL per il nuovorecord inserito
	$id_inserito = mysql_insert_id();

	// chiudo la connessione a MySQL
	mysql_close();
	
 
  
  echo json_encode($ar);
?>