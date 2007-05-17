----------------------------
-- ScieloIndex schema --
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
        prefix text NULL,
        firstname text NOT NULL,
        middlename text NULL,
        lastname text NOT NULL,
        suffix text NULL,
        degree text NULL,
        created_on timestamp DEFAULT CURRENT_TIMESTAMP,
        updated_on timestamp DEFAULT CURRENT_TIMESTAMP,
        PRIMARY KEY (id),
        UNIQUE(firstname, middlename, lastname, suffix)
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
                ON DELETE CASCADE
                ON UPDATE CASCADE
                DEFERRABLE,
        number text NULL,
        volume text NULL,
        year integer NOT NULL,
        created_on timestamp DEFAULT CURRENT_TIMESTAMP,
        updated_on timestamp DEFAULT CURRENT_TIMESTAMP,
        PRIMARY KEY (id),
        UNIQUE(journal_id, number, volume, year)
);

CREATE TABLE articles (
        id SERIAL,
        title text NOT NULL,
        journal_issue_id int4 NOT NULL
                REFERENCES journal_issues(id)
                ON UPDATE CASCADE
                ON DELETE CASCADE
                DEFERRABLE,
        fpage text NULL,
        lpage text NULL,
        page_range text NULL,
        url text NULL,
        pacsnum text NULL,
        other text NULL,
        created_on timestamp DEFAULT CURRENT_TIMESTAMP,
        updated_on timestamp DEFAULT CURRENT_TIMESTAMP,
        PRIMARY KEY (id),
        UNIQUE(title, journal_issue_id)
);

CREATE TABLE associated_files (
        id SERIAL,
        article_id int4 NOT NULL
                REFERENCES articles(id)
                ON UPDATE CASCADE
                ON DELETE CASCADE
                DEFERRABLE,
        filename text NOT NULL,
        pdfdata bytea NOT NULL,
        htmldata bytea NOT NULL,
        created_on timestamp DEFAULT CURRENT_TIMESTAMP,
        updated_on timestamp DEFAULT CURRENT_TIMESTAMP,
        PRIMARY KEY (id),
        UNIQUE(filename)
);

CREATE TABLE article_authors (
        id SERIAL,
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
        author_order int4 NOT NULL,
        created_on timestamp DEFAULT CURRENT_TIMESTAMP,
        updated_on timestamp DEFAULT CURRENT_TIMESTAMP,
        PRIMARY KEY (id),
        UNIQUE(article_id, author_id)
);

CREATE TABLE cites (
        id SERIAL,
        article_id int4 NOT NULL
                REFERENCES articles(id)
                ON UPDATE CASCADE
                ON DELETE CASCADE
                DEFERRABLE,
        cited_by_article_id int4 NOT NULL
                REFERENCES articles(id)
                ON UPDATE CASCADE
                ON DELETE CASCADE
                DEFERRABLE,
        cite_order int4 NOT NULL,
        created_on timestamp DEFAULT CURRENT_TIMESTAMP,
        updated_on timestamp DEFAULT CURRENT_TIMESTAMP,
        PRIMARY KEY (id),
        UNIQUE(article_id, cited_by_article_id)
);