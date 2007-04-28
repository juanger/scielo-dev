require File.dirname(__FILE__) + '/../test_helper'

class AuthorInstitutionTest < Test::Unit::TestCase
  fixtures :author_institutions
  fixtures :authors
  fixtures :institutions

  def setup
    @author_institutions = [:monoipn, :hectorunam]
  end

  # RIGHT
  def test_creating_author_institutions_from_fixtures
    @author_institutions.each { |author_institution|
      @author_institution = author_institutions(author_institution)
      @author_institution_db = AuthorInstitution.find(@author_institution.id)
      assert_equal @author_institution.id, @author_institution_db.id
      assert_equal @author_institution.author_id, @author_institution_db.author_id
      assert_equal @author_institution.institution_id, @author_institution_db.institution_id
    }
  end

  def test_updating
    @author_institutions.each { |author_institution|
      @author_institution = author_institutions(author_institution)
      @author_institution_db = AuthorInstitution.find(@author_institution.id)
      @author_institution_db.id = @author_institution_db.id
      assert @author_institution_db.update
      @author_institution_db.author_id = @author_institution_db.author_id + 1
      assert @author_institution_db.update
      @author_institution_db.institution_id = @author_institution_db.institution_id + 1
    }
  end

  def test_deleting
    @author_institutions.each { |author_institution|
      @author_institution = author_institutions(author_institution)
      # FIXME: Buscar en la API una metodo de busqueda que no de excepcion si no encuentra por el id.
      @author_institution_db = AuthorInstitution.find_by_author_id(@author_institution.author_id)
      assert @author_institution_db.destroy
      @author_institution_db = AuthorInstitution.find_by_author_id(@author_institution.author_id)
      assert @author_institution_db.nil?
    }
  end

  def test_creating_empty_author_institution
    @author_institution = AuthorInstitution.new()
    assert !@author_institution.save
  end

  def test_checking_uniqueness
    @author_institution = AuthorInstitution.new({:id => 3, :author_id => 1, :institution_id => 1})
    assert !@author_institution.save
  end

  # Boundary
  def test_bad_values_for_id
    @author_institution = AuthorInstitution.new({:id => 3, :author_id => 1, :institution_id => 2})

    # Checking for ID constraints
    @author_institution.id = nil
    assert @author_institution.valid?

    @author_institution.id = -2
    assert !@author_institution.valid?

    @author_institution.id = 5.6
    assert !@author_institution.valid?
  end

  def test_bad_values_for_author_id
    @author_institution = AuthorInstitution.new({:id => 3, :author_id => 1, :institution_id => 2})

    # Checking for ID constraints
    @author_institution.author_id = nil
    assert !@author_institution.valid?

    @author_institution.author_id = -2
    assert !@author_institution.valid?

    @author_institution.author_id = 5.6
    assert !@author_institution.valid?
  end

  def test_bad_values_for_institution_id
    @author_institution = AuthorInstitution.new({:id => 3, :author_id => 1, :institution_id => 2})

    # Checking for ID constraints
    @author_institution.institution_id = nil
    assert !@author_institution.valid?

    @author_institution.institution_id = -2
    assert !@author_institution.valid?

    @author_institution.institution_id = 5.6
    assert !@author_institution.valid?
  end

  def test_has_and_belongs_to_many_author
    @author_institution = AuthorInstitution.new({ :id => 3, :author_id => 2, :institution_id => 1})

    @institution = institutions(:unam)
    assert @institution.authors.size, 2
  end

  def test_has_and_belongs_to_many_institution
    @author_institution = AuthorInstitution.new({ :id => 3, :author_id => 1, :institution_id => 2})

    @author = authors(:hector)
    assert @author.institutions.size, 2
  end
end
