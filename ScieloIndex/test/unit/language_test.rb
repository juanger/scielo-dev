require File.dirname(__FILE__) + '/../test_helper'

class LanguageTest < Test::Unit::TestCase
  fixtures :languages

  def setup
    @languages = [:klingon, :quenya, :sindarin]
    @mylanguage = {:name => "defeño", :code => 'df'}
  end

    # RIGHT
  def test_creating_languages_from_fixtures
    @languages.each { |language|
      @language = languages(language)
      @language_db = Language.find_by_code(@language.code)
      assert_equal @language.name, @language_db.name
      assert_equal @language.code, @language_db.code
    }
  end

  def test_updating
    @languages.each { |language|
      @language = languages(language)
      @language_db = Language.find_by_code(@language.code)
      @language_db.name.reverse
      assert @language_db.update
      @language_db.code.reverse
      assert @language_db.update
    }
   end

  def test_deleting
    @languages.each { |language|
      @language = languages(language)
      @language_db = Language.find_by_code(@language.code)

      assert_equal @language.id, @language_db.id
      assert @language_db.destroy
      @language_db = Language.find_by_code(@language.code)
      assert @language_db.nil?
    }
  end

  def test_creating_empty_language
    @language = Language.new()
    assert !@language.save
  end

  def test_checking_uniqueness_code
    @language = Language.new(@mylanguage)
    @language.id = 124
    @language.code = 'klg'
    assert !@language.save
  end

  # Boundary
  def test_bad_values_for_id
    @language = Language.new(@mylanguage)

    # Checking for ID constraints
    @language.id = nil
    assert @language.valid?

    @language.id = -2
    assert !@language.valid?

    @language.id = 5.6
    assert !@language.valid?
  end

  def test_bad_values_for_name
    @language = Language.new(@mylanguage)

    # Checking for name constraints
    @language.name = nil
    assert !@language.valid?

    @language.name = "Ce"
    assert @language.valid?

    @language.name = "A" * 31
    assert !@language.valid?

    @language.name = "China2"
    assert !@language.valid?
  end

  def test_bad_values_for_code
    @language = Language.new(@mylanguage)

    # Checking for language code constraints
    @language.code = nil
    assert !@language.valid?

    @language.code = "c"
    assert !@language.valid?

    @language.code = "CC"
    assert !@language.valid?

    @language.code = "CCC"
    assert !@language.valid?

    @language.code = "cccc"
    assert !@language.valid?

    @language.code = "c2"
    assert !@language.valid?
  end
end
