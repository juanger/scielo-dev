require File.dirname(__FILE__) + '/../test_helper'

class CollectionTest < Test::Unit::TestCase
  fixtures :collections, :publishers, :countries

  def setup
    @collections = [:atmosfera, :medicina]
    @mycollection = {:id => 3, :title => 'Technology Review', :country_id => 840, :publisher_id => 1, :state => 'Texas', :city => 'Houston', :other => 'For ultra cool kids'}
  end

  # RIGHT CRUD (Create, Update and Delete)
  def test_creating_collections_from_fixtures
    @collections.each { |collection|
      @collection = collections(collection)
      @collection_db = Collection.find_by_title(@collection.title)
      assert_equal @collection.id, @collection_db.id
      assert_equal @collection.title, @collection_db.title
      assert_equal @collection.publisher_id, @collection_db.publisher_id
      assert_equal @collection.state, @collection_db.state
      assert_equal @collection.city, @collection_db.city
      assert_equal @collection.country_id, @collection_db.country_id
      assert_equal @collection.url, @collection_db.url
      assert_equal @collection.email, @collection_db.email
      assert_equal @collection.other, @collection_db.other
    }
  end

  def test_updating
    @collections.each { |collection|
      @collection = collections(collection)
      @collection_db = Collection.find_by_title(@collection.title)
      @collection_db.title.reverse!
      assert @collection_db.update
      @collection_db.id = @collection_db.id
      assert @collection_db.update
      @collection_db.country_id = @collection_db.country_id
      assert @collection_db.update
      @collection_db.publisher_id = @collection_db.publisher_id
      assert @collection_db.update
      @collection_db.state.reverse!
      assert @collection_db.update
      @collection_db.city.reverse!
      assert @collection_db.update
      @collection_db.url.reverse!
      assert @collection_db.update
      @collection_db.other.reverse!
      assert @collection_db.update
      @collection_db.email.reverse!
      assert @collection_db.update
    }
  end

  def test_deleting
    @collections.each { |collection|
      @collection = collections(collection)
      @collection_db = Collection.find_by_title(@collection.title)
      assert @collection_db.destroy
      @collection_db = Collection.find_by_title(@collection.title)
      assert @collection_db.nil?
    }
  end

  def test_creating_empty_collection
    @collection = Collection.new()
    assert !@collection.save
  end

  # Boundary
  def test_bad_values_for_id
    @collection = Collection.new(@mycollection)

    # Checking for ID constraints
    @collection.id = nil
    assert @collection.valid?

    @collection.id = -2
    assert !@collection.valid?

    @collection.id = 5.6
    assert !@collection.valid?
  end

  def test_bad_values_for_title
    @collection = Collection.new(@mycollection)

    # Checking title constraints
    @collection.title = nil
    assert !@collection.valid?

    @collection.title = "Fo"
    assert !@collection.valid?

    @collection.title = "A"*101
    assert !@collection.valid?

    @collection.title = "Lars1"
    assert !@collection.valid?
  end

  def test_bad_values_for_publisher_id
    @collection = Collection.new(@mycollection)

    # Checking publisher_id constraints
    @collection.publisher_id = nil
    assert !@collection.valid?

    @collection.publisher_id = 10000
    assert !@collection.valid?

    @collection.publisher_id = -2
    assert !@collection.valid?

    @collection.publisher_id = 2.5
    assert !@collection.valid?
  end

  def test_bad_values_for_state
    @collection = Collection.new(@mycollection)

    # Checking state constraints
    @collection.state = nil
    assert @collection.valid?

    @collection.state = ""
    assert @collection.valid?

    @collection.state = "A"*31
    assert !@collection.valid?

    @collection.state = "Texas1"
    assert !@collection.valid?
  end

  def test_bad_values_for_city
    @collection = Collection.new(@mycollection)

    # Checking city constraints
    @collection.city = nil
    assert @collection.valid?

    @collection.city = ""
    assert @collection.valid?

    @collection.city = "A"*31
    assert !@collection.valid?

    @collection.city = "Las Vegas1"
    assert !@collection.valid?
  end

  def test_bad_values_for_country_id
    @collection = Collection.new(@mycollection)

    # Checking country_id constraints
    @collection.country_id = nil
    assert !@collection.valid?

    @collection.country_id = 1000
    assert !@collection.valid?

    @collection.country_id = -2
    assert !@collection.valid?

    @collection.country_id = 10.6
    assert !@collection.valid?
  end

  def test_bad_values_for_url
    @collection = Collection.new(@mycollection)

    # Checking url constraints
    @collection.url = nil
    assert @collection.valid?

    @collection.url = ""
    assert @collection.valid?

    @collection.url = "A"*201
    assert !@collection.valid?

    @collection.url = "http://[]"
    assert !@collection.valid?
  end

  def test_bad_values_for_email
    @collection = Collection.new(@mycollection)

    # Checking email constraints
    @collection.email = nil
    assert @collection.valid?

    @collection.email = ""
    assert @collection.valid?

    @collection.email = "A"*21
    assert !@collection.valid?

    @collection.email = "foo@[].com"
    assert !@collection.valid?
  end


  def test_bad_values_for_other
    @collection = Collection.new(@mycollection)

    # Checking other constraints
    @collection.other = nil
    assert @collection.valid?

    @collection.other = ""
    assert @collection.valid?

    @collection.other = "A"*201
    assert !@collection.valid?

    @collection.other = "Cool kinds like comics |foo|"
    assert !@collection.valid?
  end

  def test_belongs_to_country
    @collection = Collection.new(@mycollection)
    assert @collection.country.id, 840
    assert @collection.country.name, 'Estados Unidos'
    assert @collection.country.code, 'US'
  end

  def test_belongs_to_publisher
    @collection = Collection.new(@mycollection)
    assert @collection.publisher.id, 1
    assert @collection.publisher.name, 'MIT Press'
    assert @collection.publisher.descr, 'Cool books for cool guy'
    assert @collection.publisher.url, 'http://www.mit.edu'
  end
end
