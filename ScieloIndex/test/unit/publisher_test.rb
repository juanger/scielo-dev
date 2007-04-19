require File.dirname(__FILE__) + '/../test_helper'

class PublisherTest < Test::Unit::TestCase
  fixtures :publishers

  def setup
    @publishers = [:mit, :white, :black]
  end

  # RIGHT
  def test_creating_publishers_from_fixtures
    @publishers.each { |publisher|
      @publisher = publishers(publisher)
      @publisher_db = Publisher.find_by_name(@publisher.name)
      assert_equal @publisher.id, @publisher_db.id
      assert_equal @publisher.name, @publisher_db.name
      assert_equal @publisher.descr, @publisher_db.descr
      assert_equal @publisher.url, @publisher_db.url
    }
  end

  def test_updating
    @publishers.each { |publisher|
      @publisher = publishers(publisher)
      @publisher_db = Publisher.find_by_name(@publisher.name)
      @publisher_db.name.reverse!
      assert @publisher_db.update
      @publisher_db.id = @publisher_db.id
      assert @publisher_db.update
      @publisher_db.descr.reverse!
      assert @publisher_db.update
    }
  end

  def test_deleting
    @publishers.each { |publisher|
      @publisher = publishers(publisher)
      @publisher_db = Publisher.find_by_name(@publisher.name)
      assert @publisher_db.destroy
      @publisher_db = Publisher.find_by_name(@publisher.name)
      assert @publisher_db.nil?
    }
  end

  def test_creating_empty_publisher
    @publisher = Publisher.new()
    assert !@publisher.save
  end

  def test_checking_uniqueness
    @publisher = Publisher.new({:id => 4, :name => 'MIT Press', :descr => 'For cool kids'})
    assert !@publisher.save
  end

  # Boundary
  def test_bad_values_for_id
    @publisher = Publisher.new({:id => 100, :name => 'Marvel Comics', :descr => 'Cool comics', :url => 'http://www.marvel.com'})
    
    # Checking for ID constraints
    @publisher.id = nil
    assert @publisher.valid?

    @publisher.id = -2
    assert !@publisher.valid?

    @publisher.id = 5.6
    assert !@publisher.valid?
  end

  def test_bad_values_for_name
    @publisher = Publisher.new({:id => 100, :name => 'Marvel Comics', :descr => 'Cool comics', :url => 'http://www.marvel.com'})

    # Checking name constraints
    @publisher.name = nil
    assert !@publisher.valid?

    @publisher.name = "Fo"
    assert !@publisher.valid?

    @publisher.name = "A"*101
    assert !@publisher.valid?

    @publisher.name = "Lars1"
    assert !@publisher.valid?
  end

  def test_bad_values_for_descr
    @publisher = Publisher.new({:id => 100, :name => 'Marvel Comics', :descr => 'Cool comics', :url => 'http://www.marvel.com'})

    # Checking descr constraints
    @publisher.descr = nil
    assert @publisher.valid?

    @publisher.descr = "Nice stories"
    assert @publisher.valid?

    @publisher.descr = "A"*501
    assert !@publisher.valid?

    @publisher.descr = "Lars1|"
    assert !@publisher.valid?
  end

  def test_bad_values_for_url
    @publisher = Publisher.new({:id => 100, :name => 'Marvel Comics', :descr => 'Cool comics', :url => 'http://www.marvel.com'})

    # Checking url constraints
    @publisher.url = nil
    assert @publisher.valid?

    @publisher.url = "http://foo.org"
    assert @publisher.valid?

    @publisher.url = "A"*201
    assert !@publisher.valid?

    @publisher.url = "http://[]"
    assert !@publisher.valid?
  end
end
