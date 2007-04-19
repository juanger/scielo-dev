require File.dirname(__FILE__) + '/../test_helper'

class CountryTest < Test::Unit::TestCase
  fixtures :countries
  
  def setup
    @countries = [:mexico, :brasil]
  end

  # RIGHT
  def test_creating_countries_from_fixtures
    @countries.each { |country|
      @country = countries(country)
      @country_db = Country.find_by_name(@country.name)
      assert_equal @country.name, @country_db.name
      assert_equal @country.id, @country_db.id
      assert_equal @country.code, @country_db.code
    }
  end
  def test_updating
    @countries.each { |country|
      @country = countries(country)
      @country_db = Country.find_by_name(@country.name)
      @country_db.name.reverse
      assert @country_db.update
      @country_db.id = @country_db.id + 1
      assert @country_db.update
      @country_db.code.reverse
      assert @country_db.update
      #puts @country_db.code.reverse
    }
   end

  def test_deleting
    @countries.each { |country|
      @country = countries(country)
      @country_db = Country.find_by_name(@country.name)
      assert @country_db.destroy
      @country_db = Country.find_by_name(@country.name)
      assert @country_db.nil?
    }
  end

  def test_creating_empty_country
    @country = Country.new()
    assert !@country.save
  end 

  def test_checking_uniqueness
    @country = Country.new({:id => 484, :name => 'México', :code => 'MX'})
    assert !@country.save
  end 

  # Boundary
  def test_bad_values_for_id
    @country = Country.new({:id => 156, :name => 'China', :code => 'CN'})
    
    # Checking for ID constraints
    @country.id = nil
    assert !@country.valid?

    @country.id = -2
    assert !@country.valid?

    @country.id = 5.6
    assert !@country.valid?
  end

  def test_bad_values_for_name
    @country = Country.new({:id => 156, :name => 'China', :code => 'CN'})
    
    # Checking for name constraints
    @country.name = nil
    assert !@country.valid?

    @country.name = "Ce"
    assert !@country.valid?

    @country.name = "A" * 81
    assert !@country.valid?

    @country.name = "China2"
    assert !@country.valid?
  end

  def test_bad_values_for_code
    @country = Country.new({:id => 156, :name => 'China', :code => 'CN'})
    
    # Checking for country code constraints
    @country.code = nil
    assert !@country.valid?

    @country.code = "C"
    assert !@country.valid?

    @country.code = "AAAA"
    assert !@country.valid?

    @country.code = "A2"
    assert !@country.valid?
  end
end
