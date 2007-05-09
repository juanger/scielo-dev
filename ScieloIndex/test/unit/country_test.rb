require File.dirname(__FILE__) + '/../test_helper'

class CountryTest < Test::Unit::TestCase
  fixtures :countries, :collections, :institutions

  def setup
    @countries = [:mexico, :brasil, :usa]
    @mycountry = {:id => 484, :name => 'México', :code => 'MX'}
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
    @country = Country.new(@mycountry)
    assert !@country.save
  end

  # Boundary
  def test_bad_values_for_id
    @country = Country.new(@mycountry)

    # Checking for ID constraints
    @country.id = nil
    assert !@country.valid?

    @country.id = -2
    assert !@country.valid?

    @country.id = 5.6
    assert !@country.valid?
  end

  def test_bad_values_for_name
    @country = Country.new(@mycountry)

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
    @country = Country.new(@mycountry)

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

  def test_has_many_collections
    @country = Country.find(484)
    assert_equal @country.collections[1].id, 1
    assert_equal @country.collections[1].title, 'Atmosfera'
    assert_equal @country.collections[1].email, 'atmosfera@dgb.com'
  end

  def test_has_many_institutions
    @country = Country.find(484)
    assert_equal @country.institutions[0].id, 2
    assert_equal @country.institutions[0].abbrev, 'IPN'
    assert_equal @country.institutions[0].zipcode, '45100'
  end
end
