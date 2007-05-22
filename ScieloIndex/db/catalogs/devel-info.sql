INSERT INTO institutions (name, url, abbrev, parent_id, address, country_id, state, city, zipcode, phone, fax, other) VALUES ('Universidad Nacional Autónoma de México', 'http://www.unam.mx', 'UNAM', NULL, 'Ciudad Universitaria, Delegacion Coyoacan', 484, 'Distrito Federal', 'Ciudad de México', '04510', '5622 3970', '5622 3950', NULL);

INSERT INTO authors (prefix, firstname, middlename, lastname, suffix, degree) VALUES ('Mr.', 'Héctor', 'Enrique', 'Gómez', NULL, 'Lic.');
INSERT INTO authors (prefix, firstname, middlename, lastname, suffix, degree) VALUES ('Mr.', 'Lars', '', 'Adame', NULL, 'Lic.');
INSERT INTO authors (prefix, firstname, middlename, lastname, suffix, degree) VALUES (NULL, 'David', 'Ezequiel Enoch', 'Garcia', NULL, 'PhD.');

INSERT INTO author_institutions (author_id, institution_id) VALUES (1, 1);
INSERT INTO author_institutions (author_id, institution_id) VALUES (2, 1);
INSERT INTO author_institutions (author_id, institution_id) VALUES (3, 1);

INSERT INTO publishers (name, descr, url) VALUES ('Universidad Nacional Autónoma de México', 'La universdidad mas reconocida en America Latina', 'http://www.unam.mx');

INSERT INTO journals (title, country_id, state, city, publisher_id, url, email, abbrev, issn, other) VALUES ('Atmosfera', 484, 'Distrito Federal', 'Ciudad de México', 1, 'http://www.ejournal.unam.mx/atmosfera/atmosfera_index.html', 'ejadmin@gmail.com', 'ATM', '1234-1234', NULL);
INSERT INTO journals (title, country_id, state, city, publisher_id, url, email, abbrev, issn, other) VALUES ('Revista Mexicana de Física', 484, 'Distrito Federal', 'Ciudad de México', 1, 'http://www.smf.mx/revista/indice.html_path', 'rmf@smf2.fciencias.unam.mx', 'RMF', '1235-1235', NULL);

INSERT INTO journal_issues (journal_id, number, volume, year) VALUES (1, 10, 8, 2007);
INSERT INTO journal_issues (journal_id, number, volume, year) VALUES (1, 11, 7, 2007);
INSERT INTO journal_issues (journal_id, number, volume, year) VALUES (2, 12, 1, 2007);

INSERT INTO articles (title, journal_issue_id, fpage, lpage, page_range, url, pacsnum, other) VALUES ('Calentamiento Global', 2, '100', '120', '100-120', NULL, NULL, NULL);
INSERT INTO articles (title, journal_issue_id, fpage, lpage, page_range, url, pacsnum, other) VALUES ('Classification of thunderstorm and non-thunderstorm days in Calcutta (India) on the basis of linear discriminant analysis', 1, '1', '12', '1-12', NULL, NULL, NULL);
INSERT INTO articles (title, journal_issue_id, fpage, lpage, page_range, url, pacsnum, other) VALUES ('Lightning induced heating of the ionosphere', 1, '31', '38', '31-38', NULL, NULL, NULL);
INSERT INTO articles (title, journal_issue_id, fpage, lpage, page_range, url, pacsnum, other) VALUES ('La Fisica ante el Calentamiento Global', 3, '76', '91', '76-91', NULL, NULL, NULL);
INSERT INTO articles (title, journal_issue_id, fpage, lpage, page_range, url, pacsnum, other) VALUES ('The genum Pachyphytum', 1, '45', '50', '45-50', NULL, NULL, NULL);
INSERT INTO articles (title, journal_issue_id, fpage, lpage, page_range, url, pacsnum, other) VALUES ('Arqueoastronomía en Yucatán', 3, '21', '60', '21-60', NULL, NULL, NULL);
INSERT INTO articles (title, journal_issue_id, fpage, lpage, page_range, url, pacsnum, other) VALUES ('Mayan astronomy', 2, '2', '21', '2-21', NULL, NULL, NULL);
INSERT INTO articles (title, journal_issue_id, fpage, lpage, page_range, url, pacsnum, other) VALUES ('La física en los huracanes', 3, '12', '54', '12-54', NULL, NULL, NULL);
INSERT INTO articles (title, journal_issue_id, fpage, lpage, page_range, url, pacsnum, other) VALUES ('Impact of Solar Storms in the magnetic field', 3, '34', '46', '34-46', NULL, NULL, NULL);
INSERT INTO articles (title, journal_issue_id, fpage, lpage, page_range, url, pacsnum, other) VALUES ('Carbon dioxide as a cause of ligthing storms', 2, '12', '34', '12-34', NULL, NULL, NULL);
INSERT INTO articles (title, journal_issue_id, fpage, lpage, page_range, url, pacsnum, other) VALUES ('Chalk dust and lung cancer', 3, '25', '26', '25-26', NULL, NULL, NULL);
INSERT INTO articles (title, journal_issue_id, fpage, lpage, page_range, url, pacsnum, other) VALUES ('Laptops and fertility in men', 1, '67', '82', '67-82', NULL, NULL, NULL);
INSERT INTO articles (title, journal_issue_id, fpage, lpage, page_range, url, pacsnum, other) VALUES ('Junk 101', 2, '34', '41', '34-41', NULL, NULL, NULL);


INSERT INTO associated_files (article_id, filename, pdf_path, html_path) VALUES (1, 'v17n01a01','public/associated_files/1/v17n01a01.pdf', 'public/associated_files/1/v17n01a01.htm');
INSERT INTO associated_files (article_id, filename, pdf_path, html_path) VALUES (2, 'v17n01a03','public/associated_files/2/v17n01a03.pdf', 'public/associated_files/2/v17n01a03.htm');
INSERT INTO associated_files (article_id, filename, pdf_path, html_path) VALUES (3, 'v18n1a01','public/associated_files/3/v18n1a01.pdf', 'public/associated_files/3/v18n1a01.htm');
INSERT INTO associated_files (article_id, filename, pdf_path, html_path) VALUES (4, 'v18n1a02','public/associated_files/4/v18n1a02.pdf', 'public/associated_files/4/v18n1a02.htm');
INSERT INTO associated_files (article_id, filename, pdf_path, html_path) VALUES (5, 'v17n4a01','public/associated_files/5/v17n4a01.pdf', 'public/associated_files/5/v17n4a01.htm');
INSERT INTO associated_files (article_id, filename, pdf_path, html_path) VALUES (6, 'v17n4a02','public/associated_files/6/v17n4a02.pdf', 'public/associated_files/6/v17n4a02.htm');
INSERT INTO associated_files (article_id, filename, pdf_path, html_path) VALUES (7, 'v17n3a01','public/associated_files/7/v17n3a01.pdf', 'public/associated_files/7/v17n3a01.htm');
INSERT INTO associated_files (article_id, filename, pdf_path, html_path) VALUES (8, 'v17n3a02','public/associated_files/8/v17n3a02.pdf', 'public/associated_files/8/v17n3a02.htm');
INSERT INTO associated_files (article_id, filename, pdf_path, html_path) VALUES (9, 'v18n2a01','public/associated_files/9/v18n2a01.pdf', 'public/associated_files/9/v18n2a01.htm');
INSERT INTO associated_files (article_id, filename, pdf_path, html_path) VALUES (10, 'v18n2a02','public/associated_files/10/v18n2a02.pdf', 'public/associated_files/10/v18n2a02.htm');
INSERT INTO associated_files (article_id, filename, pdf_path, html_path) VALUES (11, 'v18n3a01','public/associated_files/11/v18n3a01.pdf', 'public/associated_files/11/v18n3a01.htm');
INSERT INTO associated_files (article_id, filename, pdf_path, html_path) VALUES (12, 'v18n3a02','public/associated_files/12/v18n3a02.pdf', 'public/associated_files/12/v18n3a02.htm');
INSERT INTO associated_files (article_id, filename, pdf_path, html_path) VALUES (13, 'v18n4a01','public/associated_files/13/v18n4a01.pdf', 'public/associated_files/13/v18n4a01.htm');



INSERT INTO article_authors (article_id, author_id, author_order) VALUES (1, 1, 1);
INSERT INTO article_authors (article_id, author_id, author_order) VALUES (2, 1, 1);
INSERT INTO article_authors (article_id, author_id, author_order) VALUES (3, 1, 1);
INSERT INTO article_authors (article_id, author_id, author_order) VALUES (4, 1, 1);
INSERT INTO article_authors (article_id, author_id, author_order) VALUES (5, 1, 1);
INSERT INTO article_authors (article_id, author_id, author_order) VALUES (6, 1, 1);
INSERT INTO article_authors (article_id, author_id, author_order) VALUES (7, 1, 1);
INSERT INTO article_authors (article_id, author_id, author_order) VALUES (8, 1, 1);
INSERT INTO article_authors (article_id, author_id, author_order) VALUES (9, 1, 1);
INSERT INTO article_authors (article_id, author_id, author_order) VALUES (10, 1, 1);
INSERT INTO article_authors (article_id, author_id, author_order) VALUES (11, 1, 1);
INSERT INTO article_authors (article_id, author_id, author_order) VALUES (12, 1, 1);
INSERT INTO article_authors (article_id, author_id, author_order) VALUES (13, 1, 1);
INSERT INTO article_authors (article_id, author_id, author_order) VALUES (1, 2, 2);
INSERT INTO article_authors (article_id, author_id, author_order) VALUES (1, 3, 3);
INSERT INTO article_authors (article_id, author_id, author_order) VALUES (3, 2, 2);
INSERT INTO article_authors (article_id, author_id, author_order) VALUES (4, 3, 2);

INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (1, 2, 1);
INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (1, 3, 2);
INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (1, 4, 3);
INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (1, 5, 4);
INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (1, 6, 5);
INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (1, 7, 6);
INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (1, 8, 7);
INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (1, 9, 8);
INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (1, 10, 9);
INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (1, 11, 10);
INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (1, 12, 11);
INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (1, 13, 12);

INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (2, 3, 1);
INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (2, 4, 2);
INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (2, 5, 3);
INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (2, 6, 4);
INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (2, 7, 5);
INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (2, 8, 6);
INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (2, 9, 7);
INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (2, 10, 8);
INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (2, 11, 9);
INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (2, 12, 10);
INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (2, 13, 11);

INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (3, 4, 1);
INSERT INTO cites (article_id, cited_by_article_id, cite_order) VALUES (3, 5, 2);
