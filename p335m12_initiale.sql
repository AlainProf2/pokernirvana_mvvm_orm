update toursParole set DEC_J0='PETIT_BLIND', DEC_J1='GROS_BLIND', DEC_J2='Attente', DEC_J3='Attente' where num_partie=335 and num_main=12
update etapes set prochainjoueur=2 where num_partie=335 and num_main=12
delete from historique where numero_evenement=63496