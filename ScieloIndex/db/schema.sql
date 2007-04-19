----------------------------
-- Articles and journals --
----------------------------
CREATE TABLE countries ( 
    	id INTEGER NOT NULL,
    	name text NOT NULL,
	code char(3) NOT NULL, 
	PRIMARY KEY(id),
	UNIQUE(name),
	UNIQUE(code)
);
COMMENT ON TABLE countries IS
	'Listado de países';
COMMENT ON COLUMN countries.name IS
	'Nombre del país';
COMMENT ON COLUMN countries.code IS
	'Abreviación (3 letras) del país';

CREATE TABLE institutions (  
        id SERIAL,
        name text NOT NULL,
        url  text NULL,
        abbrev text NULL,
	parent_id integer NULL
            	REFERENCES institutions(id) 
            	ON UPDATE CASCADE           
            	ON DELETE CASCADE           
            	DEFERRABLE,
	address text NULL,
	country_id int4 NOT NULL 
            	REFERENCES countries(id) 
            	ON UPDATE CASCADE           
            	DEFERRABLE,
        state text NULL,
        city text NULL,
	zipcode text NULL,
	phone text NULL,
	fax text NULL,
        other text NULL,
 	created_on timestamp DEFAULT CURRENT_TIMESTAMP,
	updated_on timestamp DEFAULT CURRENT_TIMESTAMP,
        PRIMARY KEY(id),
	UNIQUE(name, country_id)
); 
COMMENT ON TABLE institutions IS
	'Instituciones';
COMMENT ON COLUMN institutions.parent_id IS
	'Institución padre, para expresar jerarquías (p.ej. UNAM es la 
	institución padre de IIEc)';

CREATE TABLE authors (
	id SERIAL,
	firstname text NOT NULL,
	middlename text NULL,
	lastname text NOT NULL,
	suffix text NULL,
 	created_on timestamp DEFAULT CURRENT_TIMESTAMP,
	updated_on timestamp DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY (id)
);

CREATE TABLE author_institutions (
	id SERIAL,
	author_id integer NOT NULL
     		REFERENCES authors(id) 
            	ON UPDATE CASCADE           
            	ON DELETE CASCADE           
            	DEFERRABLE,
	institution_id integer NOT NULL 
            	REFERENCES institutions(id) 
            	ON UPDATE CASCADE           
            	DEFERRABLE,
 	created_on timestamp DEFAULT CURRENT_TIMESTAMP,
	updated_on timestamp DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY(id),
	UNIQUE(author_id, institution_id)
);

CREATE TABLE mediatypes ( 
	id SERIAL,
	name text NOT NULL,
	created_on timestamp DEFAULT CURRENT_TIMESTAMP,
	updated_on timestamp DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY (id),
	UNIQUE(name)
);
COMMENT ON TABLE mediatypes  IS 
	'Tipo de medio: Impreso, digital, etc.';

CREATE TABLE publishers ( 
	id SERIAL,
	name text NOT NULL,
	descr text NULL,
	url text NULL,
	created_on timestamp DEFAULT CURRENT_TIMESTAMP,
	updated_on timestamp DEFAULT CURRENT_TIMESTAMP,		
	PRIMARY KEY (id),
	UNIQUE(name)
);
COMMENT ON TABLE publishers IS
	'Editoriales';

CREATE TABLE collections (
	id SERIAL,
	title text NOT NULL,
	country_id int4 NOT NULL 
        	REFERENCES countries(id) 
            	ON UPDATE CASCADE           
            	DEFERRABLE,
        state text NULL,
        city text NULL,
	publisher_id int4 NOT NULL 
        	REFERENCES publishers(id) 
            	ON UPDATE CASCADE           
            	DEFERRABLE,
        url  text NULL,
        email text NULL,
        other text NULL,           
	created_on timestamp DEFAULT CURRENT_TIMESTAMP,
	updated_on timestamp DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY (id)
);

CREATE TABLE journals (
	id SERIAL,
        abbrev text NULL,      
	issn text NULL,
	PRIMARY KEY (id),
	UNIQUE(issn) 
) INHERITS (collections);
COMMENT ON TABLE journals IS
	'Revistas en las cuales pueden publicarse artículos';

CREATE TABLE journal_issues (
	id SERIAL,
	journal_id int4 NOT NULL 
        	REFERENCES journals 
            	ON UPDATE CASCADE
            	DEFERRABLE,
	number text NOT NULL,
	volume text NOT NULL,
	year integer NOT NULL,
	PRIMARY KEY (id),
	UNIQUE(journal_id, number, volume, year)
);

CREATE TABLE articles (
	id SERIAL,
	title text NOT NULL,
	pages text NULL,
	url text NULL,
	pacsnum text NULL,
	other text NULL,
--	status_id
	PRIMARY KEY (id)
);

CREATE TABLE article_authors (
	id SERIAL,
	journalissue_id int4 NOT NULL 
        	REFERENCES journal_issues(id) 
            	ON UPDATE CASCADE         
            	ON DELETE CASCADE         
            	DEFERRABLE,
	article_id int4 NOT NULL 
        	REFERENCES articles(id) 
            	ON UPDATE CASCADE
            	ON DELETE CASCADE                 
            	DEFERRABLE,
	author_id int4 NOT NULL 
        	REFERENCES authors(id) 
            	ON UPDATE CASCADE
            	ON DELETE CASCADE   
            	DEFERRABLE,
	PRIMARY KEY (id),
	UNIQUE(journalissue_id, article_id, author_id)
);
-- CREATE TABLE researchareas ( 
-- 	id SERIAL,
-- 	name text NOT NULL,
-- 	descr text NULL
-- 	created_on timestamp DEFAULT CURRENT_TIMESTAMP,
-- 	updated_on timestamp DEFAULT CURRENT_TIMESTAMP,
-- 	PRIMARY KEY (id),
-- 	UNIQUE(name)
-- );
-- COMMENT ON TABLE researchareas IS 
-- 	'Áreas del conocimiento a los que responde una revista:
-- 	 Genéricos: Ciencias sociales, ciencias naturales, humanidades
-- 	 Específicos: (desglose :)';

-- CREATE TABLE journalstatistics (
-- 	id SERIAL,
-- 	journal_id int4 NOT NULL 
-- 	    REFERENCES journals(id)      
--             ON UPDATE CASCADE
-- 	    DEFERRABLE,
-- 	totalcites integer NULL,
-- 	impactfactor float NULL, 	
-- 	immediacy   text NULL,
-- 	dateupdate date NULL, 
--         moduser_id int4 NULL               	    -- Use it to known who
--             REFERENCES users(id)            -- has inserted, updated or deleted
--             ON UPDATE CASCADE               -- data into or  from this table.
--             DEFERRABLE,
--         created_on timestamp DEFAULT CURRENT_TIMESTAMP,
--         updated_on timestamp DEFAULT CURRENT_TIMESTAMP,
-- 	PRIMARY KEY (id)
-- );
-- COMMENT ON TABLE journalstatistics IS
-- 	'Estadísticas asociadas a la revista';
-- COMMENT ON COLUMN journalstatistics.dateupdate IS
-- 	'Fecha en que fue actualizado el factor de impacto/inmediatez';


-- CREATE TABLE journal_publicationcategories ( 
-- 	id SERIAL,
-- 	journal_id int4 NOT NULL 
--             REFERENCES journals(id)      
--             ON UPDATE CASCADE
--             DEFERRABLE,
--     	publicationcategory_id int4 NOT NULL 
--             REFERENCES publicationcategories(id)      
--             ON UPDATE CASCADE
--             DEFERRABLE,
-- 	moduser_id int4 NULL               	    -- Use it to known who
--             REFERENCES users(id)            -- has inserted, updated or deleted
--             ON UPDATE CASCADE               -- data into or  from this table.
--             DEFERRABLE,
-- 	created_on timestamp DEFAULT CURRENT_TIMESTAMP,
-- 	updated_on timestamp DEFAULT CURRENT_TIMESTAMP,
-- 	PRIMARY KEY (id),
-- 	UNIQUE(journal_id, publicationcategory_id)
-- );
-- COMMENT ON TABLE journal_publicationcategories IS
-- 	'Categorías (áreas del conocimiento) a las que pertenece una revista';

-- CREATE TABLE roleinjournals (
-- 	id serial,
-- 	name text NOT NULL,
-- 	moduser_id int4 NULL               	    -- Use it to known who
--             REFERENCES users(id)            -- has inserted, updated or deleted
--             ON UPDATE CASCADE               -- data into or  from this table.
--             DEFERRABLE,
-- 	created_on timestamp DEFAULT CURRENT_TIMESTAMP,
-- 	updated_on timestamp DEFAULT CURRENT_TIMESTAMP,
-- 	PRIMARY KEY(id),
-- 	UNIQUE (name)
-- );
-- COMMENT ON TABLE roleinjournals IS
-- 	'Roles que un usuario puede tener en una publicación:
-- 	Editor, Compilador, Revisor, Arbitro, Otro';


-- CREATE TABLE user_journals ( 
-- 	id SERIAL,
--     	user_id int4 NOT NULL 
--         	REFERENCES users(id)      
--             	ON UPDATE CASCADE
--             	ON DELETE CASCADE   
--             	DEFERRABLE,
-- 	journal_id int4 NOT NULL 
--         	REFERENCES journals(id)
--             	ON UPDATE CASCADE
--             	DEFERRABLE,
-- 	roleinjournal_id int4 NOT NULL 
--         	REFERENCES roleinjournals(id)
--             	ON UPDATE CASCADE
--             	DEFERRABLE,
-- 	startyear int4 NOT NULL,
-- 	startmonth int4 NULL CHECK (startmonth >= 1 AND startmonth <= 12),
-- 	endyear   int4 NULL,
-- 	endmonth int4 NULL CHECK (endmonth >=1 AND endmonth <= 12),
-- 	other text NULL,
-- 	moduser_id int4 NULL               	    -- Use it to known who
--             REFERENCES users(id)            -- has inserted, updated or deleted
--             ON UPDATE CASCADE               -- data into or  from this table.
--             DEFERRABLE,
-- 	created_on timestamp DEFAULT CURRENT_TIMESTAMP,
-- 	updated_on timestamp DEFAULT CURRENT_TIMESTAMP,
-- 	PRIMARY KEY (id),
-- 	UNIQUE(user_id, journal_id, roleinjournal_id)
-- --	CONSTRAINT valid_duration CHECK (endyear IS NULL OR
-- --	       (startyear * 12 + coalesce(startmonth,	0)) > (endyear * 12 + coalesce(endmonth,0)))
-- );
-- COMMENT ON TABLE user_journals IS
-- 	'Relación entre usuarios del sistema y las publicaciones';
-- COMMENT ON COLUMN user_journals.roleinjournal_id IS
-- 	'Es el rol que tiene el usuario en la publicación';

-- CREATE TABLE articlestatuses (  
-- 	id SERIAL, 
-- 	name varchar(50) NOT NULL,
-- 	PRIMARY KEY (id),
-- 	UNIQUE(name)
-- );
-- COMMENT ON TABLE articlestatuses IS
-- 	'Estado de un artículo (utilizado en articles y newspaperarticles):
-- 	Publicado, en prensa, enviado, aceptado, en proceso, ...';

-- CREATE TABLE articles ( 
--     id SERIAL,
--     title text NOT NULL,
--     journal_id int4 NOT NULL 
--             REFERENCES journals(id)      
--             ON UPDATE CASCADE
--             DEFERRABLE,
--     articlestatus_id int4 NOT NULL  
--             REFERENCES articlestatuses(id)      
--             ON UPDATE CASCADE
--             DEFERRABLE,
--     pages text NULL,   
--     year  int4 NOT NULL,
--     month  int4 NULL CHECK (month >= 1 AND month <= 12),
--     vol text NULL,
--     num text NULL,
--     authors text NOT NULL ,
--     url text NULL,
--     pacsnum text NULL,
--     other text NULL,
--     moduser_id int4 NULL               	    -- Use it to known who
--             REFERENCES users(id)            -- has inserted, updated or deleted
--             ON UPDATE CASCADE               -- data into or  from this table.
--             DEFERRABLE,
--     created_on timestamp DEFAULT CURRENT_TIMESTAMP,
--     updated_on timestamp DEFAULT CURRENT_TIMESTAMP,
--     PRIMARY KEY (id),
--     UNIQUE(title, journal_id, year)
-- );
-- COMMENT ON TABLE articles IS
-- 	'Datos de un artículo publicado';
-- COMMENT ON COLUMN articles.authors IS
-- 	'Listado de autores tal cual aparece en el artículo - La relación 
-- 	entre usuarios y artículos es independiente de esta, ver 
-- 	authorarticles.';

-- CREATE TABLE user_articles ( 
--     id SERIAL,
--     user_id int4 NOT NULL 
--             REFERENCES users(id)      
--             ON UPDATE CASCADE
--             ON DELETE CASCADE   
--             DEFERRABLE,
--     article_id int4 NOT NULL 
--             REFERENCES articles(id)
--             ON UPDATE CASCADE
--             DEFERRABLE,
--     ismainauthor BOOLEAN NOT NULL default 't',
--     other text NULL,
--     moduser_id int4 NULL               	    -- Use it to known who
--             REFERENCES users(id)            -- has inserted, updated or deleted
--             ON UPDATE CASCADE               -- data into or  from this table.
--             DEFERRABLE,
--     created_on timestamp DEFAULT CURRENT_TIMESTAMP,
--     updated_on timestamp DEFAULT CURRENT_TIMESTAMP,
--     PRIMARY KEY (user_id, article_id)
-- );
-- COMMENT ON TABLE user_articles IS
-- 	'Relación entre usuarios del sistema y artículos';
-- COMMENT ON COLUMN user_articles.ismainauthor IS
-- 	'Basta con señalar si el usuario es autor principal o es coautor';

-- CREATE TABLE articleslog (
--     id SERIAL, 
--     article_id integer NOT NULL 
--             REFERENCES articles(id)
--             ON UPDATE CASCADE
--             ON DELETE CASCADE
--             DEFERRABLE,
--     old_articlestatus_id integer NOT NULL 
--             REFERENCES articlestatuses(id)
--             ON UPDATE CASCADE
--             DEFERRABLE,
--     changedate date NOT NULL default now()::date,
--     moduser_id integer NULL      -- It will be used only to know who has
--             REFERENCES users(id) -- inserted, updated or deleted  
--             ON UPDATE CASCADE    -- data into or from this table.
--             DEFERRABLE,
--     created_on timestamp DEFAULT CURRENT_TIMESTAMP,
-- 	updated_on timestamp DEFAULT CURRENT_TIMESTAMP,
--     PRIMARY KEY (id)
-- );
-- COMMENT ON TABLE articleslog IS
-- 	'Estado actual (y bitácora) de un artículo - Cuándo fue enviado, 
-- 	cuándo fue aceptado, etc.';

-- CREATE TABLE file_articles (
--    id serial NOT NULL,
--    article_id int4 NOT NULL
--             REFERENCES articles(id)
--             ON UPDATE CASCADE
--             DEFERRABLE,
--    filename text NOT NULL,
--    conten_type text NULL,
--    content bytea NOT NULL,
--    creationtime timestamp NOT NULL DEFAULT now(),
--    lastmodiftime timestamp NOT NULL DEFAULT now(),
--    moduser_id int4 NULL      -- It will be used only to know who has
--             REFERENCES users(id) -- inserted, updated or deleted  
--             ON UPDATE CASCADE    -- data into or from this table.
--             DEFERRABLE,
--     created_on timestamp DEFAULT CURRENT_TIMESTAMP,
--     updated_on timestamp DEFAULT CURRENT_TIMESTAMP,
--     PRIMARY KEY (id),
--     UNIQUE (article_id, filename)
-- );
-- COMMENT ON TABLE file_articles IS
-- 	'Archivos relacionados a los artículos';
-- COMMENT ON COLUMN file_articles.article_id IS
-- 	'ID del artículo referenciado';
-- COMMENT ON COLUMN file_articles.content IS
-- 	'Contenido (binario) del archivo';



------
-- Update articleslog if there was a status change
------
-- CREATE OR REPLACE FUNCTION articles_update() RETURNS TRIGGER 
-- SECURITY DEFINER AS '
-- DECLARE 
-- BEGIN
-- 	IF OLD.articlestatus_id = NEW.articlestatus_id THEN
-- 		RETURN NEW;
-- 	END IF;
-- 	INSERT INTO articleslog (articles_id, old_articlestatus_id, moduser_id)
-- 		VALUES (OLD.id, OLD.articlestatus_id, OLD.moduser_id);
--         RETURN NEW;
-- END;
-- ' LANGUAGE 'plpgsql';

-- CREATE TRIGGER articles_update BEFORE DELETE ON articles
-- 	FOR EACH ROW EXECUTE PROCEDURE articles_update();
