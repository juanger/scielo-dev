require File.dirname(__FILE__) + '/../test_helper'

class CountryTest < Test::Unit::TestCase
  fixtures :countries
  
  def setup
    @countries = [:mexico, :brasil]
  end

  # Replace this with your real tests.
  def test_created_countries_from_fixtures
    @countries.each { |country|
      @country = countries(country)
      @country_db = Country.find_by_name(@country.name)
      assert_equal @country.name, @country_db.name
      assert_equal @country.id, @country_db.id
      assert_equal @country.code, @country_db.code
    }
  end

  def test_update_countries
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

  def test_delete_countries
    @countries.each { |country|
      @country = countries(country)
      @country_db = Country.find_by_name(@country.name)
      assert @country_db.destroy
      @country_db = Country.find_by_name(@country.name)
      assert @country_db.nil?
    }
  end
  #def teardown
  #end
end
