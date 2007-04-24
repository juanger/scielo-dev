require File.dirname(__FILE__) + '/../test_helper'
class CollectionTest < Test::Unit::TestCase
  fixtures :collections

  def setup
    @collections = [:atmosfera, :medicina]
  end

  # RIGHT CRUD (Create, Update and Delete)
  def test_creating_collections_from_fixtures
    @collections.each { |collection|
      @collection = collections(collection)
      @collection_db = Collection.find_by_title(@collection.title)
      assert_equal @collection.id, @collection_db.id
      assert_equal @collection.title, @collection_db.title
      assert_equal @collection.country_id, @collection_db.country_id
      assert_equal @collection.state, @collection_db.state
      assert_equal @collection.city, @collection_db.city
      assert_equal @collection.publisher_id, @collection_db.publisher_id
      assert_equal @collection.url, @collection_db.url
      assert_equal @collection.email, @collection_db.email
      assert_equal @collection.other, @collection_db.other
      }
   end

  def test_updating
      @collections.each { |collection|
      @collection = collections(collection)
      @collection_db = Collection.find_by_title(@collection.title)
      #@collection_db.id = @collection_db.id + 1 #Es serial, se cambia?
      #assert @collection_db.update
      @collection_db.title.reverse
      assert @collection_db.update
      @collection_db.country_id = @collection_db.country_id + 1
      assert @collection_db.update
      @collection_db.state.reverse
      assert @collection_db.update
      @collection_db.city.reverse
      assert @collection_db.update
      @collection_db.publisher_id = @collection_db.publisher_id + 1
      assert @collection_db.update
      @collection_db.url.reverse
      assert @collection_db.update
      @collection_db.email.reverse
      assert @collection_db.update
      @collection_db.other.reverse
      assert @collection_db.update
      #puts @collection_db.code.reverse
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

  def test_checking_uniqueness
    @collection = Collection.new({:id => 1, :title => 'Atmósfera', :country_id => 484})
    assert !@collection.save
  end

# Boundary
  def test_bad_values_for_id
  #@collection is the object, here is created
    @collection = Collection.new({:id => 1, :title => 'Atmósfera', :country_id => 484, :state => 'Distrito Federal', :city => 'Ciudad de México', :publisher_id => 40, :url => 'www.atmosfera.com', :email => 'atmosfera@dgb.com', :other => 'en proceso'})
    # Checking for empty ID 
    @collection.id = nil
    assert !@collection.valid?
  end

  def test_bad_values_for_title_and_country_id
  #@collection is the object, here is created
    @collection = Collection.new({:id => 1, :title => 'Atmósfera', :country_id => 484, :state => 'Distrito Federal', :city => 'Ciudad de México', :publisher_id => 40, :url => 'www.atmosfera.com', :email => 'atmosfera@dgb.com', :other => 'en proceso'})
    # Checking for empty values constraints
    @collection.title = nil
    assert !@collection.valid?

    @collection.country_id = nil
    assert !@collection.valid?

  end

end
