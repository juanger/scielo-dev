require File.dirname(__FILE__) + '/../test_helper'

class AuthorTest < Test::Unit::TestCase
  fixtures :authors
  
  def setup
    @authors = [:hector, :memo, :mono]
  end 

  # RIGHT
  def test_creating_authors_from_fixtures
    @authors.each { |author|
      @author = authors(author)
      @author_db = Author.find_by_firstname(@author.firstname)
      assert_equal @author.id, @author_db.id
      assert_equal @author.firstname, @author_db.firstname
      assert_equal @author.middlename, @author_db.middlename
      assert_equal @author.lastname, @author_db.lastname
      assert_equal @author.suffix, @author_db.suffix
    }
  end

  def test_updating
    @authors.each { |author|
      @author = authors(author)
      @author_db = Author.find_by_firstname(@author.firstname)
      @author_db.firstname.reverse
      assert @author_db.update
      @author_db.id = @author_db.id + 1
      assert @author_db.update
      @author_db.lastname.reverse
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

  # Boundary
  def test_bad_values_for_id
    @author = Author.new({:id => 100, :firstname => 'Lars', :lastname => 'Adame'})
    
    # Checking for ID constraints
    @author.id = nil
    assert @author.valid?

    @author.id = -2
    assert !@author.valid?

    @author.id = 5.6
    assert !@author.valid?
  end

  def test_bad_values_for_firstname
    @author = Author.new({:id => 100, :firstname => 'Lars', :lastname => 'Adame'})

    # Checking first name constraints
    @author.firstname = nil
    assert !@author.valid?

    @author.firstname = "Fo"
    assert !@author.valid?

    @author.firstname = "A"*31
    assert !@author.valid?

    @author.firstname = "Lars1"
    assert !@author.valid?
  end

  def test_bad_values_for_lastname
    @author = Author.new({:id => 100, :firstname => 'Lars', :lastname => 'Adame'})

    # Checking last name constraints
    @author.lastname = nil
    assert !@author.valid?

    @author.lastname = "Fo"
    assert !@author.valid?

    @author.lastname = "A"*31
    assert !@author.valid?

    @author.lastname = "Adame1"
    assert !@author.valid?
  end

  def test_bad_values_for_middlename
    @author = Author.new({:id => 100, :firstname => 'Lars', :lastname => 'Adame'})

    # Checking middle name constraints
    #@author.middlename = nil
    assert @author.valid?

    @author.middlename = ""
    assert @author.valid?

    @author.middlename = "A"*21
    assert !@author.valid?

    @author.middlename = "F2"
    assert !@author.valid?
  end

  def test_bad_values_for_suffix
    @author = Author.new({:id => 100, :firstname => 'Lars', :lastname => 'Adame'})

    # Checking middle name constraints
    #@author.suffix = nil
    assert @author.valid?

    @author.suffix = ""
    assert @author.valid?

    @author.suffix = "A"*9
    assert !@author.valid?

    @author.suffix = "F2"
    assert !@author.valid?
  end
end
