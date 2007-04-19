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
      @publisher_db.name.reverse
      assert @publisher_db.update
      @publisher_db.id = @publisher_db.id + 1
      assert @publisher_db.update
      @publisher_db.descr.reverse
      assert @publisher_db.update
    }
  end
end
