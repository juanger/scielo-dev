require File.dirname(__FILE__) + '/../test_helper'

class InstitutionTest < Test::Unit::TestCase
  fixtures :institutions
  
  def setup
    @institutions = [:unam]
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
      @institution_db.parent_id = @institution_db.parent_id + 1
      assert @institution_db.update
      @institution_db.address.reverse
      assert @institution_db.update
      @institution_db.country_id = @institution_db.country_id + 1
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
    @institution = Institution.new({:id => 1, :name => 'Universidad Nacional Autónoma de México', :country_id => 484})
    assert !@institution.save
  end

# Boundary
  def test_bad_values_for_id
  #@institution is the object, here is created
    @institution = Institution.new({:id => 1, :name => 'Universidad Nacional Autónoma de México', :url => 'http://www.unam.mx', :abbrev => 'UNAM', :parent_id => 1, :address => 'Ciudad Universitaria, Delegación Coyoacán', :country_id => 484, :state => 'Distrito Federal', :city => 'México', :zipcode => '04510', :phone => ' ', :fax => ' ',  :other => ' ' })

    # Checking for empty ID 
    @institution.id = nil
    assert !@institution.valid?
  end

  def test_bad_values_for_name_and_country_id
  #@institution is the object, here is created
    @institution = Institution.new({:id => 1, :name => 'Universidad Nacional Autónoma de México', :url => 'http://www.unam.mx', :abbrev => 'UNAM', :parent_id => 1, :address => 'Ciudad Universitaria, Delegación Coyoacán', :country_id => 484, :state => 'Distrito Federal', :city => 'México', :zipcode => '04510', :phone => ' ', :fax => ' ',  :other => ' ' })
    # Checking for empty values constraints
    @institution.name = nil
    assert !@institution.valid?

    @institution.country_id = nil
    assert !@institution.valid?

  end
end
