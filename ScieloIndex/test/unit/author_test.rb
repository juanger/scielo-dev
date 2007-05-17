require File.dirname(__FILE__) + '/../test_helper'

class AuthorTest < Test::Unit::TestCase
  fixtures :countries, :journals, :journal_issues, :articles, :authors, :institutions, :author_institutions, :article_authors

  def setup
    @authors = [:hector, :memo, :mono]
    @myauthor = {:prefix => 'Mr.', :firstname => 'Hector', :lastname => 'Gomez', :degree => 'Lic.'}
  end

  # RIGHT
  def test_creating_authors_from_fixtures
    @authors.each { |author|
      @author = authors(author)
      @author_db = Author.find_by_firstname(@author.firstname)
      assert_equal @author.id, @author_db.id
      assert_equal @author.prefix, @author_db.prefix
      assert_equal @author.firstname, @author_db.firstname
      assert_equal @author.middlename, @author_db.middlename
      assert_equal @author.lastname, @author_db.lastname
      assert_equal @author.suffix, @author_db.suffix
      assert_equal @author.degree, @author_db.degree
    }
  end

  def test_updating
    @authors.each { |author|
      @author = authors(author)
      @author_db = Author.find_by_firstname(@author.firstname)
      @author_db.firstname.reverse!
      assert @author_db.update
      @author_db.id = @author_db.id
      assert @author_db.update
      @author_db.lastname.reverse!
      assert @author_db.update
    }
  end

  def test_deleting
    @authors.each { |author|
      @author = authors(author)
      @author_db = Author.find_by_firstname(@author.firstname)
      assert @author_db.destroy
      @author_db = Author.find_by_firstname(@author.firstname)
      assert @author_db.nil?
    }
  end

  def test_creating_empty_author
    @author = Author.new()
    assert !@author.save
  end

  def test_uniqueness
    @author = Author.new(@myauthor)
    @author.middlename = 'Enrique'
    assert !@author.save

    @author.suffix = 'Jr.'
    assert @author.save
  end

  # Boundary
  def test_bad_values_for_id
    @author = Author.new(@myauthor)

    # Checking for ID constraints
    @author.id = nil
    assert @author.valid?

    @author.id = -2
    assert !@author.valid?

    @author.id = 5.6
    assert !@author.valid?
  end

  def test_bad_values_for_prefix
    @author = Author.new(@myauthor)

    # Checking first name constraints
    @author.prefix = nil
    assert @author.valid?

    @author.prefix = ""
    assert @author.valid?

    @author.prefix = "A"*11
    assert !@author.valid?

    @author.prefix = "Mr1"
    assert !@author.valid?
  end

  def test_bad_values_for_firstname
    @author = Author.new(@myauthor)

    # Checking first name constraints
    @author.firstname = nil
    assert !@author.valid?

    @author.firstname = "Fo"
    assert !@author.valid?

    @author.firstname = "A"*31
    assert !@author.valid?

    @author.firstname = "Hector1"
    assert !@author.valid?
  end

  def test_bad_values_for_lastname
    @author = Author.new(@myauthor)

    # Checking last name constraints
    @author.lastname = nil
    assert !@author.valid?

    @author.lastname = "Fo"
    assert !@author.valid?

    @author.lastname = "A"*31
    assert !@author.valid?

    @author.lastname = "Gomez1"
    assert !@author.valid?
  end

  def test_bad_values_for_middlename
    @author = Author.new(@myauthor)

    # Checking middle name constraints
    #@author.middlename = nil
    assert @author.valid?

    @author.middlename = ""
    assert @author.valid?

    @author.middlename = "A"*101
    assert !@author.valid?

    @author.middlename = "F2"
    assert !@author.valid?
  end

  def test_bad_values_for_suffix
    @author = Author.new(@myauthor)

    # Checking suffix constraints
    #@author.suffix = nil
    assert @author.valid?

    @author.suffix = ""
    assert @author.valid?

    @author.suffix = "A"*11
    assert !@author.valid?

    @author.suffix = "F2"
    assert @author.valid?
  end

  def test_bad_values_for_degree
    @author = Author.new(@myauthor)

    # Checking degree constraints
    #@author.degree = nil
    assert @author.valid?

    @author.degree = ""
    assert @author.valid?

    @author.degree = "A"*11
    assert !@author.valid?

    @author.degree = "F2"
    assert !@author.valid?
  end

  def test_has_many_articles
    @author = Author.find(1)
    assert_equal @author.articles[0].title, 'Classification of thunderstorm and non-thunderstorm days in Calcutta (India) on the basis of linear discriminant analysis'
    assert_equal @author.articles[0].page_range, '3-12'
    assert_equal @author.articles[0].url, 'http://scielo.unam.mx/scielo.php?script=sci_arttext&pid=S0187-62362004000100001&lng=es&nrm=iso&tlng=en'
  end

  def test_has_many_institutions
    @author = Author.find(1)
    assert_equal @author.institutions[0].id, 1
    assert_equal @author.institutions[0].abbrev, 'UNAM'
    assert_equal @author.institutions[0].city,'México'
  end

  def test_as_vancouver
    @author = Author.find(1)
    assert_equal @author.as_vancouver, 'Gomez HE'
    @author = Author.find(2)
    assert_equal @author.as_vancouver, 'Giron GN'
  end
end
