require File.dirname(__FILE__) + '/../test_helper'

class InstitutionTest < Test::Unit::TestCase
  fixtures :countries, :institutions, :authors, :author_institutions

  def setup
    @institutions = [:unam, :ipn, :nyu]
    @myinstitution = {:id => 4, :name => 'University of California: San Diego', :url => 'http://www.ucsd.edu', :abbrev => 'UCSD', :parent_id => 4, :address => 'Nobel Drive #12, La Jolla', :country_id => 840, :state => 'California', :city => 'San Diego', :zipcode => '04511', :phone => ' ', :fax => ' ',  :other => ' '}
  end

  # RIGHT CRUD (Create, Update and Delete)
  def test_creating_institutions_from_fixtures
    @institutions.each { |institution|
      @institution = institutions(institution)
      @institution_db = Institution.find_by_name(@institution.name)
      assert_equal @institution.id, @institution_db.id
      assert_equal @institution.name, @institution_db.name
      assert_equal @institution.url, @institution_db.url
      assert_equal @institution.abbrev, @institution_db.abbrev
      assert_equal @institution.parent_id, @institution_db.parent_id
      assert_equal @institution.address, @institution_db.address
      assert_equal @institution.country_id, @institution_db.country_id
      assert_equal @institution.state, @institution_db.state
      assert_equal @institution.city, @institution_db.city
      assert_equal @institution.zipcode, @institution_db.zipcode
      assert_equal @institution.phone, @institution_db.phone
      assert_equal @institution.fax, @institution_db.fax
      assert_equal @institution.other, @institution_db.other
    }
   end

  def test_updating
      @institutions.each { |institution|
      @institution = institutions(institution)
      @institution_db = Institution.find_by_name(@institution.name)
      #@institution_db.id = @institution_db.id + 1 Es serial, se cambia?
      assert @institution_db.update
      @institution_db.name.reverse
      assert @institution_db.update
      @institution_db.url.reverse
      assert @institution_db.update
      @institution_db.abbrev.reverse
      assert @institution_db.update
      @institution_db.parent_id = @institution_db.parent_id
      assert @institution_db.update
      @institution_db.address.reverse
      assert @institution_db.update
      @institution_db.country_id = @institution_db.country_id
      assert @institution_db.update
      @institution_db.state.reverse
      assert @institution_db.update
      @institution_db.city.reverse
      assert @institution_db.update
      @institution_db.zipcode.reverse
      assert @institution_db.update
      @institution_db.phone.reverse
      assert @institution_db.update
      @institution_db.fax.reverse
      assert @institution_db.update
      @institution_db.other.reverse
      assert @institution_db.update
      #puts @institution_db.code.reverse
    }
   end

  def test_deleting
    @institutions.each { |institution|
      @institution = institutions(institution)
      @institution_db = Institution.find_by_name(@institution.name)
      assert @institution_db.destroy
      @institution_db = Institution.find_by_name(@institution.name)
      assert @institution_db.nil?
    }
  end

  def test_creating_empty_institution
    @institution = Institution.new()
    assert !@institution.save
  end

  def test_checking_uniqueness
    @institution = Institution.new(@myinstitution)
    @institution.name = 'Universidad Nacional Autónoma de México'
    @institution.country_id = '484'
    assert !@institution.save
  end

# Boundary
  def test_bad_values_for_id
    @institution = Institution.new(@myinstitution)

    # Checking for id constraints
    @institution.id = nil
    assert @institution.valid?

    @institution.id = 5.2
    assert !@institution.valid?

    @institution.id = -10
    assert !@institution.valid?
  end

  def test_bad_values_for_name
    @institution = Institution.new(@myinstitution)

    # Checking for name constraints
    @institution.name = nil
    assert !@institution.valid?
    
    @institution.name = "AA"
    assert !@institution.valid?

    @institution.name = "A"*501
    assert !@institution.valid?
  end

  def test_bad_values_for_parent_id
    @institution = Institution.new(@myinstitution)

    # Checking for parent_id constraints
    @institution.parent_id = nil
    assert @institution.valid?
    
    @institution.parent_id = 5.2
    assert !@institution.valid?

    @institution.parent_id = -10
    assert !@institution.valid?
  end

  def test_bad_values_for_url
    @institution = Institution.new(@myinstitution)

    # Checking for url constraints
    @institution.parent_id = nil
    assert @institution.valid?
    
    @institution.parent_id = ''
    assert @institution.valid?

    @institution.parent_id = 'A'*201
    assert !@institution.valid?
  end

  def test_bad_values_for_abbrev
    @institution = Institution.new(@myinstitution)

    # Checking for abbrev constraints
    @institution.parent_id = nil
    assert @institution.valid?
    
    @institution.parent_id = ''
    assert @institution.valid?

    @institution.parent_id = 'A'*201
    assert !@institution.valid?
  end

  def test_bad_values_for_address
    @institution = Institution.new(@myinstitution)

    # Checking for address constraints
    @institution.address = nil
    assert @institution.valid?
    
    @institution.address = ''
    assert @institution.valid?

    @institution.address = 'A'*201
    assert !@institution.valid?
  end

  def test_bad_values_for_country_id
    @institution = Institution.new(@myinstitution)

    # Checking for country_id constraints
    @institution.country_id = nil
    assert !@institution.valid?
    
    @institution.country_id = 5.2
    assert !@institution.valid?

    @institution.country_id = -10
    assert !@institution.valid?
  end

  def test_bad_values_for_state
    @institution = Institution.new(@myinstitution)

    # Checking for state constraints
    @institution.state = nil
    assert @institution.valid?
    
    @institution.state = ''
    assert @institution.valid?

    @institution.state = 'A'*201
    assert !@institution.valid?
  end

  def test_bad_values_for_city
    @institution = Institution.new(@myinstitution)

    # Checking for city constraints
    @institution.city = nil
    assert @institution.valid?
    
    @institution.city = ''
    assert @institution.valid?

    @institution.city = 'A'*201
    assert !@institution.valid?
  end

  def test_bad_values_for_zipcode
    @institution = Institution.new(@myinstitution)

    # Checking for zipcode constraints
    @institution.zipcode = nil
    assert @institution.valid?
    
    @institution.zipcode = ''
    assert @institution.valid?

    @institution.zipcode = 'A'*51
    assert !@institution.valid?
  end

  def test_bad_values_for_phone
    @institution = Institution.new(@myinstitution)

    # Checking for phone constraints
    @institution.phone = nil
    assert @institution.valid?
    
    @institution.phone = ''
    assert @institution.valid?

    @institution.phone = 'A'*51
    assert !@institution.valid?
  end

  def test_bad_values_for_fax
    @institution = Institution.new(@myinstitution)

    # Checking for fax constraints
    @institution.fax = nil
    assert @institution.valid?
    
    @institution.fax = ''
    assert @institution.valid?

    @institution.fax = 'A'*51
    assert !@institution.valid?
  end

  def test_bad_values_for_other
    @institution = Institution.new(@myinstitution)

    # Checking for other constraints
    @institution.other = nil
    assert @institution.valid?
    
    @institution.other = ''
    assert @institution.valid?

    @institution.other = 'A'*101
    assert !@institution.valid?
  end

  def test_belongs_to_country
    @institution = Institution.find(1)
    assert_equal @institution.country.id, 484
    assert_equal @institution.country.name, 'México'
    assert_equal @institution.country.code, 'MX'
  end

  def test_belongs_to_parent_institutions
    institution_fixture = institutions(:unam)
    @institution = Institution.new({:name => 'Direccion General de Bibliotecas', :url => 'http://www.dgb.unam.mx', :abbrev => 'DGB', :parent_id => institution_fixture.id, :address => 'Ciudad Universitaria, Delegacion Coyocan', :country_id => 484, :state => 'Distrito Federal', :city => 'México', :zipcode => '04510', :phone => ' ', :fax => ' ',  :other => ' '})
    assert_equal @institution.parent_institution.name, institution_fixture.name
    assert_equal @institution.parent_institution.abbrev, institution_fixture.abbrev
    assert_equal @institution.parent_institution.state, institution_fixture.state
  end

  def test_many_authors
    @institution = Institution.find(1)
    @author = @institution.authors.find(1)
    assert_equal @author.firstname, 'Hector'
    assert_equal @author.lastname, 'Reyes'
    assert_equal @author.suffix, 'Mr.'

    @author = @institution.authors.find(2)
    assert_equal @author.firstname, 'Guillermo'
    assert_equal @author.lastname, 'Giron'
    assert_equal @author.suffix, 'PhD.'
  end
end
