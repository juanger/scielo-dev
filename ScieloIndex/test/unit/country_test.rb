require File.dirname(__FILE__) + '/../test_helper'

class CountryTest < Test::Unit::TestCase
  fixtures :countries

  # Replace this with your real tests.
  def test_mx
    @country = countries(:mexico)
    assert_equal @country.name, 'México'
  end
end
